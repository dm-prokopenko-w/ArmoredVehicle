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
using Random = UnityEngine.Random;

namespace LevelsSystem
{
    public class LevelsController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private EnemyController _enemyController;
        [Inject] private ItemController _itemController;

        private bool _isPlay;
        private Dictionary<int, List<LevelView>> _levelViews = new();
        private List<LevelView> _curLevelViews = new();
        private float _speed;
        private LevelsConfig _dataLevels;
        private Transform _parent;
        private int _curLvl;
        private int _maxLvl;

        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnGameWin += GameWin;
            _gameplay.OnGameOver += DespawnCurLvl;
            _gameplay.OnResetGame += () => SpawnLevels(0);

            _dataLevels = _assetLoader.LoadConfig(Constants.LevelsConfigPath) as LevelsConfig;
            _maxLvl = _dataLevels.Levels.Count - 1;
            _parent = _itemController.GetTransformParent(Constants.ParentLevels);

            SpawnLevels(_curLvl = 0);
        }

        private void GameWin()
        {
            _curLvl++;

            if (_maxLvl < _curLvl)
            {
                _curLvl = 0;
            }

            SpawnLevels(_curLvl);
        }

        private void SpawnLevels(int num)
        {
            _speed = GetLevelSpeed(num);
            DespawnCurLvl();
            if (!_levelViews.ContainsKey(num))
            {
                var levelsPrefab = GetLevelViews(num);
                List<LevelView> levelsView = new();
                List<Enemy> enemies = new();
                int posZ = 0;

                foreach (var level in levelsPrefab)
                {
                    Debug.LogError(Random.insideUnitCircle);
                    
                    var lvl = Object.Instantiate(level, new Vector3(0, 0, posZ), Quaternion.identity, _parent);
                    enemies.AddRange(lvl.Init());

                    levelsView.Add(lvl);
                    posZ += Constants.LevelStep;
                }

                _enemyController.Init(enemies);

                _curLevelViews = levelsView;
                _levelViews.Add(num, levelsView);
            }
            else
            {
                _curLevelViews = _levelViews[num];

                foreach (var lvl in _curLevelViews)
                {
                    lvl.gameObject.SetActive(true);
                }
            }
        }

        private float GetLevelSpeed(int num) => _dataLevels.Levels[num].LevelSpeed;
        private List<LevelView> GetLevelViews(int num) => _dataLevels.Levels[num].LevelViews;

        private void DespawnCurLvl()
        {
            foreach (var lvl in _curLevelViews)
            {
                lvl.ResetGame();
                lvl.gameObject.SetActive(false);
            }
        }

        public void Dispose()
        {
            _gameplay.OnGameWin -= GameWin;
            _gameplay.OnGameOver -= DespawnCurLvl;
            _gameplay.OnResetGame -= () => SpawnLevels(0);
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
        }

        public void Tick()
        {
            if (!_isPlay) return;

            foreach (var lvl in _curLevelViews)
            {
                lvl.transform.Translate(Vector3.back * Time.deltaTime * _speed);
            }
        }
    }
}