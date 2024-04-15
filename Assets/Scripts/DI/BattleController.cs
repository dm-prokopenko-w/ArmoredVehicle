using EnemySystem;
using GameplaySystem;
using PlayerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleSystem
{
    public class BattleController
    {
        [Inject] private GameplayController _gameplay;

        public Action<Collider, Enemy> OnDamageEnemy;
        public Action OnKill;

        private List<Enemy> _enemies = new();
        private List<Bullet> _bullets = new();
        private Player _player;

        public void AddEnemies(List<Enemy> enemies) => _enemies.AddRange(enemies);
        public void UpdateBulletList(Bullet bullet, bool value)
        {
            if (value)
            {
                _bullets.Add(bullet);
            }
            else
            {
                _bullets.Remove(bullet);
            }
        }

        public void AddPlayer(Player player) => _player = player;

        public void DamageEnemy(Collider trigger, Enemy enemy)
        {
            if (trigger.tag.Equals("Bullet"))
            {
                enemy.TakeDamage(_player.Damage, () => OnKill?.Invoke());
            }
            else if (trigger.tag.Equals("Player"))
            {
                enemy.Dead();
            }

        }

        public void DamagePlayer(Collider col)
        {
            var enemy = _enemies.Find(x => x.Col == col);
            if (enemy == null) return;

            _player.TakeDamage(enemy.Damage, () =>
            {
                _gameplay.OnGameOver?.Invoke();
            });
        }
    }
}
