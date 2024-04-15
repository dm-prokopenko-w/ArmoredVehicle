using Core;
using System;
using UnityEngine;
using static Game.Constants;

namespace EnemySystem
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyTypes _id;

        private void Start()
        {
            var rot = UnityEngine.Random.Range(0, 359);
            transform.Rotate(0, rot, 0);
        }

        public override void Dead()
        {
            gameObject.SetActive(false);
        }

        public EnemyTypes Id => _id;
    }
}
