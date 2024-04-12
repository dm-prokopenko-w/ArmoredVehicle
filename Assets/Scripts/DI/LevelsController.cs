using System;
using System.Collections.Generic;
using Game;
using GameplaySystem;
using UISystem;
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
        [Inject] private UIController _uiController;

        private bool _isEnd = false;
        private UnityEvent OnTick = new ();

        private int _secondsBeforeSpawnMin;
        private int _secondsBeforeSpawnMax;
        
        private float _asteroidSpeedMin;
        private float _asteroidSpeedMax;
        
        private bool _isPlay;
        private List<LevelView> _levelViews = new();
        private float _speed;
        private LevelsConfig _data;
        private Transform _parent;
        
        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnGameOver += DespawnAll;
            _gameplay.OnResetGame += DespawnAll;

            _data = _assetLoader.LoadConfig(Constants.LevelsConfigPath) as LevelsConfig;

            _parent = _uiController.GetTransformParent(Constants.ParentLevels);
            
            SpawnLevels(0);
        }

        private void SpawnLevels(int num)
        {
            _speed = GetLevelSpeed(num);
            int posZ = Constants.LevelStep;
            var levels = GetLevelViews(num);
            
            foreach (var lvl in levels)
            {
                _levelViews.Add(Object.Instantiate(lvl, new Vector3(0, 0, posZ), Quaternion.identity, _parent));
                posZ += Constants.LevelStep;
            }
        }

        private float GetLevelSpeed(int num) => _data.Levels[num].LevelSpeed;
        private List<LevelView> GetLevelViews(int num) => _data.Levels[num].LevelViews;
        
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