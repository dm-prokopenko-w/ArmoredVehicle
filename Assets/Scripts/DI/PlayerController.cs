using System;
using BattleSystem;
using Core;
using ControlSystem;
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
        private float _curRotY = 180f;

        private bool _isPlay;
        private bool _isAttack;
        private Bullet _bulletPrefab;
        private Transform _parentActive;
        private CharacterItem _playerItem;
        private float _current = 0f;

        public void Start()
        {
            _gameplay.OsPlayGame += PlayGame;

            _control.TouchStart += _ => IsAttack(true);
            _control.TouchMoved += data => Looking(data);
            _control.TouchEnd += _ => IsAttack(false);
            
            _parentActive = _itemController.GetTransform(TransformViewID + TransformObject.ActiveBulletParent);
            var parentInactive = _itemController.GetTransform(TransformViewID + TransformObject.InactiveBulletParent);

            _pool = new ObjectPool<Bullet>();
            _pool.InitPool(_bulletPrefab, parentInactive);
        }

        private void PlayGame(bool value)
        {
            _isPlay = value;
            _player.ActiveGame(value);
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
            _curRotY = data.delta.x switch
            {
                > 0 when _curRotY < 270 => _player.Rotate(new Vector3(0, StepRotTurret, 0) * Time.deltaTime),
                < 0 when _curRotY > 90 => _player.Rotate(new Vector3(0, -StepRotTurret, 0) * Time.deltaTime),
                _ => _curRotY
            };
        }

        public void Dispose()
        {
            _gameplay.OsPlayGame -= PlayGame;

            _control.TouchStart -= data => IsAttack(true);
            _control.TouchMoved -= data => Looking(data);
            _control.TouchEnd -= (data) => IsAttack(false);
        }

        public void InitPlayer(Player player)
        {
            _battleController.AddPlayer(player);
            var data = _assetLoader.LoadConfig(PlayerConfigPath) as PlayerConfig;
            _bulletPrefab = data.Bullet;
            _playerItem = new CharacterItem()
            {
                HP = data.HP,
                Damage = data.Damage
            };
            
            _player = player;
            _player.Init(_playerItem, _battleController.TriggerPlayer);
        }
        
        public void Tick()
        {
            if (!_isPlay) return;
            if (!_isAttack) return;
            Timer();
        }
        
        private void Timer()
        {
            _current += Time.deltaTime;
            if (_current > SecBetweenAttack)
            {
                _current = 0;
                Attack();
            }
        }

        private void Attack()
        {
            var bullet = _pool.Spawn(_bulletPrefab, _player.StartBulletPos, _player.TurretQuat, _parentActive); 
            bullet.Move(_player.DirAttack, () =>  _pool.Despawn(bullet));
        }
    }
}