using Core;
using EnemySystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LevelsSystem
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies;

        public bool IsMoved { get; set; }
        private UnityEvent _onTick = new ();

        private float _endPosY;
        private float _speed;
        private Action<LevelView> _onDespawn;

        protected void Start()
        {
            _onTick.AddListener(Move);
        }

        public List<Enemy> Init(List<EnemyItem> items, Action<Collider, Enemy> onTrigger)
        {
            foreach (var enemy in _enemies)
            {
                var item = items.Find(x => x.Id == enemy.Id);

                if (item == null) continue;
                enemy.Init(item, (col) => onTrigger(col, enemy));
            }

            return _enemies;
        }

        private void Move()
        {
            if(!IsMoved) return;
            
            transform.Translate(Vector2.down * Time.deltaTime * _speed);

            if (!(_endPosY > transform.position.y)) return;
            IsMoved = false;
            _onDespawn?.Invoke(this);
        }
    }
}