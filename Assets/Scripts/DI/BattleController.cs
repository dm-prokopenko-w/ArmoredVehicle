using EnemySystem;
using GameplaySystem;
using PlayerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace BattleSystem
{
    public class BattleController
    {
        [Inject] private GameplayController _gameplay;
        [Inject] private ItemController _itemController;

        private List<Enemy> _enemies = new();
        private List<Bullet> _bullets = new();
        private Player _player;
        private int _killCount = 0;

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

        public void AddPlayer(Player player)
        {
            _player = player;
            SetKillsCount();
        }

        public void DamageEnemy(Collider trigger, Enemy enemy)
        {
            if (trigger.tag.Equals("Bullet"))
            {
                enemy.TakeDamage(_player.Damage, () =>
                {
                    _killCount++;
                    SetKillsCount();
                });
            }
            else if (trigger.tag.Equals("Player"))
            {
                enemy.Dead();
            }
        }

        private void SetKillsCount() => 
            _itemController.SetText(TextViewID + TextObject.KillCounter, KillsCountText + _killCount);
        
        public void TriggerPlayer(Collider trigger)
        {
            if (trigger.tag.Equals("Finish"))
            {
                _killCount = 0;
                SetKillsCount();
                _gameplay.GameWin();
                return;
            }
            var enemy = _enemies.Find(x => x.Col == trigger);
            if (enemy == null) return;

            _player.TakeDamage(enemy.Damage, () =>
            {
                _killCount = 0;
                SetKillsCount();
                _gameplay.GameOver();
            });
        }
    }
}
