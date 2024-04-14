using System;
using System.Collections.Generic;
using BattleSystem;
using EnemySystem;
using Game;
using GameplaySystem;
using ItemSystem;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace LevelsSystem
{
    public class LevelsController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private BattleController _battleController;
        [Inject] private ItemController _itemController;

        private bool _isEnd = false;
        private UnityEvent OnTick = new();

        private int _secondsBeforeSpawnMin;
        private int _secondsBeforeSpawnMax;

        private float _asteroidSpeedMin;
        private List<EnemyItem> _enemies;

        private bool _isPlay;
        private List<LevelView> _levelViews = new();
        private float _speed;
        private LevelsConfig _dataLevels;
        private Transform _parent;

        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnGameOver += DespawnAll;
            _gameplay.OnResetGame += DespawnAll;

            _dataLevels = _assetLoader.LoadConfig(Constants.LevelsConfigPath) as LevelsConfig;
            var dataEnemies = _assetLoader.LoadConfig(Constants.EnemyConfigPath) as EnemyConfig;
            _enemies = dataEnemies.Enemies;

            _parent = _itemController.GetTransformParent(Constants.ParentLevels);

            SpawnLevels(0);
        }

        private void SpawnLevels(int num)
        {
            _speed = GetLevelSpeed(num);
            int posZ = Constants.LevelStep;
            var levels = GetLevelViews(num);

            foreach (var level in levels)
            {
                var lvl = Object.Instantiate(level, new Vector3(0, 0, posZ), Quaternion.identity, _parent);
                var enemies = lvl.Init(_enemies, _battleController.DamageEnemy);
                _battleController.AddEnemies(enemies);
                _levelViews.Add(lvl);
                posZ += Constants.LevelStep;
            }
        }

        private void CheckTrigger(Collider trigger, Enemy enemy)
        {
        }

        private float GetLevelSpeed(int num) => _dataLevels.Levels[num].LevelSpeed;
        private List<LevelView> GetLevelViews(int num) => _dataLevels.Levels[num].LevelViews;

        private void DespawnAll()
        {
            _levelViews.Clear();
        }

        private void Despawn(LevelView levelView)
        {
            _levelViews.Remove(levelView);
        }

        public void Dispose()
        {
            _gameplay.OnGameOver -= DespawnAll;
            _gameplay.OnResetGame -= DespawnAll;
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
            _isEnd = true;
        }

        public void Tick()
        {
            if (!_isPlay) return;

            _parent.Translate(Vector3.back * Time.deltaTime * _speed);
        }
    }
}