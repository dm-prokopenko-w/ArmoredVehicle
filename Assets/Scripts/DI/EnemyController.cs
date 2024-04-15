using System;
using System.Collections.Generic;
using BattleSystem;
using Game;
using GameplaySystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace EnemySystem
{
    public class EnemyController : IStartable, ITickable, IDisposable
    {
        [Inject] private AssetLoader _assetLoader;
        [Inject] private BattleController _battleController;
        [Inject] private GameplayController _gameplay;

        private List<EnemyItem> _enemyItems = new ();
        private List<Enemy> _enemies = new ();
        private bool _isPlay;

        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnResetGame += ResetGame;
        }

        public void Init(List<Enemy> enemies)
        {
            var dataEnemies = _assetLoader.LoadConfig(EnemyConfigPath) as EnemyConfig;
            _enemyItems = dataEnemies.Enemies;

            _battleController.AddEnemies(enemies);
            _enemies.AddRange(enemies);
            foreach (var enemy in enemies)
            {
                var itemConfig = _enemyItems.Find(x => x.Id == enemy.Id);

                if (itemConfig == null) continue;

                enemy.Init(itemConfig, (col) => _battleController.DamageEnemy(col, enemy));
            }
        }
        
        public void Dispose()
        {
            _gameplay.OnResetGame -= ResetGame;
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
        }

        private void ResetGame()
        {
            foreach (var enemy in _enemies)
            {
                if(!enemy.gameObject.activeSelf) Debug.LogError(enemy.gameObject.name);
                enemy.gameObject.SetActive(true);
                enemy.ResetGame();
            }
        }

        public void Tick()
        {
            if (!_isPlay) return;

            foreach (var enemy in _enemies)
            {
                if(!enemy.IsActive) continue;

                float distance = Vector3.Distance (enemy.transform.position, Vector3.zero);

                if (enemy.transform.position.z < DistDead)
                {
                    enemy.Dead();   
                }
                if (distance < DistEnemyMoveToPlayer && enemy.transform.position.z > DistStopMove)
                {
                    enemy.StartMove();
                    enemy.transform.LookAt(Vector3.zero);
                    enemy.transform.Translate(-enemy.transform.position * Time.deltaTime * SpeedEnemy, Space.World);
                }
            }
        }
    }
}