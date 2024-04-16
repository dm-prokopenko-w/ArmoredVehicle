using System;
using System.Collections.Generic;
using BattleSystem;
using Game;
using GameplaySystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Game.Constants;
using Random = UnityEngine.Random;
using Core;
using ItemSystem;

namespace EnemySystem
{
    public class EnemyController : IStartable, ITickable, IDisposable
    {
        [Inject] private AssetLoader _assetLoader;
        [Inject] private BattleController _battleController;
        [Inject] private GameplayController _gameplay;
        [Inject] private ItemController _itemController;

        private List<Enemy> _enemies = new();
        private bool _isPlay;
        private ObjectPool<Enemy> _pool;

        private EnemyConfig _data;

        public void Start()
        {
            _gameplay.OsPlayGame += UpdateGame;

            _data = _assetLoader.LoadConfig(EnemyConfigPath) as EnemyConfig;

            var parentInactive = _itemController.GetTransform(TransformViewID + TransformObject.InactiveEnemyParent);
            _pool = new ObjectPool<Enemy>();

            foreach (var enemy in _data.Enemies)
            {
                _pool.InitPool(enemy.Prefab, parentInactive);
            }
        }

        public void SpawnEnemy(int count, Transform parent)
        {
            List<Enemy> enemies = new();

            for (int i = 0; i < count; i++)
            {
                var item = RandomEnemy;
                
                var posEnemy = (new Vector3(
                    parent.position.x + Random.Range(-SpawnSize, SpawnSize),
                    0,
                    parent.position.z + Random.Range(-SpawnSize, SpawnSize)));

                var enemy = _pool.Spawn(item.Prefab, posEnemy, Quaternion.identity, parent);
                enemy.Init(item, (col) => _battleController.DamageEnemy(col, enemy));
                enemies.Add(enemy);
            }
            
            _battleController.AddEnemies(enemies);
            _enemies.AddRange(enemies);
        }

        private EnemyItem RandomEnemy => _data.Enemies[Random.Range(0, _data.Enemies.Count)];

        public void Dispose()
        {
            _gameplay.OsPlayGame -= UpdateGame;
        }

        private void UpdateGame(bool value )
        {
            _isPlay = value;

            if (value) return;
            foreach (var enemy in _enemies)
            {
                enemy.ResetGame();
                _pool.Despawn(enemy);
            }
            
            _enemies.Clear();
        }

        public void Tick()
        {
            if (!_isPlay) return;

            foreach (var enemy in _enemies)
            {
                if (!enemy.IsActive) continue;

                var distance = Vector3.Distance(enemy.transform.position, Vector3.zero);

                if (enemy.transform.position.z < DistDead)
                {
                    enemy.Dead();
                }

                if (!(distance < DistEnemyMoveToPlayer) || !(enemy.transform.position.z > DistStopMove)) continue;
                enemy.StartMove();
                enemy.transform.LookAt(Vector3.zero);
                enemy.transform.Translate(-enemy.transform.position * Time.deltaTime * SpeedEnemy, Space.World);
            }
        }
    }
}