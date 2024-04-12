using System;
using System.Threading.Tasks;
using Core.ControlSystem;
using Game;
using GameplaySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace PlayerSystem
{
    public class PlayerController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private ControlModule _control;

        private Player _player;
        private bool _isMovePlayer;
        private float _dirX;
        private float _maxX;
        private bool _isPlay;

        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
           // _gameplay.OnResetGame += () => _player.transform = new Vector2(0, _player.PosY.anchoredPosition.y);

            _control.TouchStart += data => MoveShip(data, true);
            _control.TouchMoved += data => MoveShip(data, true);
            _control.TouchEnd += (data) => MoveShip(data, false);

            _maxX = Screen.width / 2 - 200;
        }

        private void MoveShip(PointerEventData data, bool isMovePlayer)
        {
            _isMovePlayer = isMovePlayer;
            var pos = Camera.main.ScreenToWorldPoint(data.position);
            _dirX = pos.x > 0 ? 1 : -1;
        }

        public void Dispose()
        {
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
            _player.OnСollision -= GameOver;
           // _gameplay.OnResetGame -= () => _player.PosY.anchoredPosition = new Vector2(0, _player.PosY.anchoredPosition.y);

            _control.TouchStart -= data => MoveShip(data, true);
            _control.TouchMoved -= data => MoveShip(data, true);
            _control.TouchEnd -= (data) => MoveShip(data, false);
        }

        
        public void InitPlayer(Player player)
        {
            _player = player;
            _player.OnСollision += GameOver;
        }

        private async void GameOver()
        {
            _gameplay.GameOver();

            await Task.Delay(1500);
            //_player.PosY.anchoredPosition = new Vector2(0, _player.PosY.anchoredPosition.y);
            _gameplay.ResetGame();
        }

        public void Tick()
        {
            if (!_isPlay) return;
            if (!_isMovePlayer) return;

           // if (_player.PosY.anchoredPosition.x > _maxX && _dirX > 0) return;
            //if (_player.PosY.anchoredPosition.x < -_maxX && _dirX < 0) return;

            _player.transform.Translate(new Vector3(_dirX, 0, 0) * Time.deltaTime);
        }
    }
}