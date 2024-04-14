using System;
using System.Threading.Tasks;
using BattleSystem;
using Core;
using Core.ControlSystem;
using Game;
using GameplaySystem;
using ItemSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace PlayerSystem
{
    public class PlayerController : IStartable, IDisposable, ITickable
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private ControlModule _control;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private ItemController _itemController;
        [Inject] private BattleController _battleController;

        private ObjectPool<Bullet> _pool;
        private Player _player;
        private bool _isMovePlayer;
        private float _dirX;
        private float _maxX;
        private bool _isPlay;
        private bool _isAttack;
        private float _stepRot = 100f;
        private Bullet _bulletPrefab;
        private Transform _bulletParentActive;
        private Transform _bulletParentInactive;
        private CharacterItem _playerItem;

        public void Start()
        {
            _gameplay.OsPlayGame += (value) => _isPlay = value;
            _gameplay.OnResetGame += () => _player.transform.position = Vector3.zero;

            _control.TouchStart += data => IsAttack(true);
            _control.TouchMoved += data => Looking(data);
            _control.TouchEnd += (data) => IsAttack(false);

            _maxX = Screen.width / 2 - 200;
        }

        private void IsAttack(bool value)
        {
            if (value)
            {
                Attack();
                _current = 0;
            }

            _isAttack = value;
        }

        private void Looking(PointerEventData data)
        {
            if (data.delta.x > 0)
            {
                _player.Rotate(new Vector3(0, _stepRot, 0) * Time.deltaTime);

            }
            else if (data.delta.x < 0)
            {
                _player.Rotate(new Vector3(0, -_stepRot, 0) * Time.deltaTime);
            }
        }

        public void Dispose()
        {
            _gameplay.OsPlayGame -= (value) => _isPlay = value;
            _player.OnСollision -= GameOver;
            _gameplay.OnResetGame -= () => _player.transform.position = Vector3.zero;

            _control.TouchStart -= data => IsAttack(true);
            _control.TouchMoved -= data => Looking(data);
            _control.TouchEnd -= (data) => IsAttack(false);
        }

        public void InitPlayer(Player player)
        {
            var data = _assetLoader.LoadConfig(PlayerConfigPath) as PlayerConfig;
            _bulletPrefab = data.Bullet;
            _playerItem = new CharacterItem()
            {
                HP = data.HP,
                Damage = data.Damage
            };

            _bulletParentActive = _itemController.GetTransformParent(BalletParentID + ObjectState.Active);
            _bulletParentInactive = _itemController.GetTransformParent(BalletParentID + ObjectState.Inactive);

            _pool = new ObjectPool<Bullet>();
            _pool.InitPool(_bulletPrefab, _bulletParentInactive);

            _player = player;
            _player.Init(_playerItem, _battleController.DamagePlayer);
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
            if (!_isAttack) return;
            Timer();
        }

        private float _secBetwenAttack = 0.5f;
        private float _current = 0f;

        private void Timer()
        {
            _current += Time.deltaTime;
            if (_current > _secBetwenAttack)
            {
                _current = 0;
                Attack();
            }
        }

        private void Attack()
        {
            var bullet = _pool.Spawn(_bulletPrefab, _player.StartBulletPos, Quaternion.identity, _bulletParentActive);
            _battleController.UpdateBulletList(bullet, true);
            bullet.Move(_player.DirAttack, () => DespawnBullt(bullet));
        }

        private void DespawnBullt(Bullet bullet)
        {
            _battleController.UpdateBulletList(bullet, false);
            _pool.Despawn(bullet);
        }
    }
}