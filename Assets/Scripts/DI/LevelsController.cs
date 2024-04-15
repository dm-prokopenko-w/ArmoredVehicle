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
using Core;
using static Game.Constants;

namespace LevelsSystem
{
    public class LevelsController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private EnemyController _enemyController;
        [Inject] private ItemController _itemController;

        private bool _isPlay;
        private List<LevelView> _curLevelViews = new();
        private float _speed;
        private LevelsConfig _data;
        private Transform _parentActive;

        private int _curViews;
        private int _maxViews;
        private int _curLvl;
        private int _maxLvls;
        private ObjectPool<LevelView> _pool;
        private UnityEvent _onTick = new ();
        private int _posZ;
        public void Start()
        {
            _gameplay.OsPlayGame += PlayGame;
            _gameplay.OnGameWin += GameWin;
            _gameplay.OnGameOver += DespawnCurLvl;
            _gameplay.OnResetGame += SpawnStartView;

            _data = _assetLoader.LoadConfig(LevelsConfigPath) as LevelsConfig;
            _maxLvls = _data.Levels.Count - 1;
            
            _parentActive = _itemController.GetTransformParent(ParentLevels + ObjectState.Active);
            var parentInactive = _itemController.GetTransformParent(ParentLevels + ObjectState.Inactive);
            _pool = new ObjectPool<LevelView>();
            
            foreach (var lvl in _data.Levels)
            {
                _pool.InitPool(lvl.StartLevelViews, parentInactive);
                _pool.InitPool(lvl.FinishLevelViews, parentInactive);

                foreach (var type in lvl.TypesLevelViews)
                {
                    _pool.InitPool(type, parentInactive);
                }
            }

            _curLvl = 0;

            SpawnStartView();
        }

        private void PlayGame(bool value)
        {
            _isPlay = value;

            if (!value) return;
            _posZ = LevelStep;

            for (int i = 0; i < 2; i++)
            {
                SpawnLvl(RandomLvl);
            }
        }

        private void SpawnLvl(LevelView prefab, bool isAddEnemy = true)
        {
            var lvl = _pool.Spawn(prefab, new Vector3(0, 0, _posZ), Quaternion.identity, _parentActive);
            _onTick.AddListener(lvl.Move);

            lvl.Init(GetLevelSpeed(), () =>
            {
                _onTick.RemoveListener(lvl.Move);
                Despawn(lvl);
            });

            if (isAddEnemy)
            {
                var countEnemy = Random.Range(_data.Levels[_curLvl].MaxCountEnemy, _data.Levels[_curLvl].MaxCountEnemy);
                _enemyController.SpawnEnemy(countEnemy, lvl.transform);
            }
            
            _posZ += LevelStep;
            _curViews++;
            _curLevelViews.Add(lvl);
        }
        
        private void GameWin()
        {
            _curLvl++;

            if (_maxLvls < _curLvl)
            {
                _curLvl = 0;
            }

            SpawnStartView();
        }

        private LevelView RandomLvl => _data.Levels[_curLvl]
            .TypesLevelViews[Random.Range(0, _data.Levels[_curLvl].TypesLevelViews.Count)];

        private void SpawnStartView()
        {
            DespawnCurLvl();
            _maxViews = _data.Levels[_curLvl].CountRandomLevels;
            SpawnLvl(_data.Levels[_curLvl].StartLevelViews, false);
        }

        private void Despawn(LevelView lvl)
        {
            _pool.Despawn(lvl);
Debug.LogError(_curViews + " - " + _maxViews);
            if (_curViews < _maxViews)
            {
                SpawnLvl(RandomLvl);
            }
            else if(_curViews == _maxViews)
            {
                SpawnLvl(_data.Levels[_curLvl].FinishLevelViews);
            }
        }

        private float GetLevelSpeed() => _data.Levels[_curLvl].LevelSpeed;

        private void DespawnCurLvl()
        {
            foreach (var lvl in _curLevelViews)
            {
                _pool.Despawn(lvl);
            }
        }

        public void Dispose()
        {
            _gameplay.OnGameWin -= GameWin;
            _gameplay.OnGameOver -= DespawnCurLvl;
            _gameplay.OnResetGame -= SpawnStartView;
            _gameplay.OsPlayGame -= PlayGame;
        }

        public void Tick()
        {
            if (!_isPlay) return;
            _onTick?.Invoke();
        }
    }
}