using Core;
using System;
using UnityEngine;
using static Game.Constants;

namespace EnemySystem
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyTypes _id;
        [SerializeField] private EnemyView _view;

        private void Start()
        {
            var rot = UnityEngine.Random.Range(0, 359);
            transform.Rotate(0, rot, 0);
        }

        public override void Init(CharacterItem item, Action<Collider> onTrigger)
        {
            base.Init(item, onTrigger);
            _view.SetHP();
        }

        public override void TakeDamage(float damage, Action onDead)
        {
            base.TakeDamage(damage, onDead);

            _view.SetHP(_hp / _maxHP);
        }

        public EnemyTypes Id => _id;
    }
}
