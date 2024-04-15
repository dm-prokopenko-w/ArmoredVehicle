﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CharacterItem
    {
        public float HP = 10;
        public float Damage = 5;
    }

    public abstract class Character: MonoBehaviour
    {
        [SerializeField] private Collider _col;
        [SerializeField] protected CharacterView _view;

        public float Damage => _damage;
        public Collider Col => _col;

        protected float _damage;
        protected float _maxHP;
        protected float _hp;
        protected Action<Collider> _onTrigger;

        public virtual void Init(CharacterItem item, Action<Collider> onTrigger)
        {
            _hp = item.HP;
            _maxHP = _hp;
            _damage = item.Damage;
            _onTrigger = onTrigger;
            _view.SetHP();
        }

        public virtual void TakeDamage(float damage, Action onDead)
        {
            _hp -= damage;

            _view.SetHP(_hp/ _maxHP);
            if (_hp <= 0)
            {
                Dead();
                onDead?.Invoke();
            }
        }

        public virtual void Dead()
        {

        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            _onTrigger?.Invoke(other);
        }
    }

    public abstract class CharacterView: MonoBehaviour
    {
        [SerializeField] protected Slider _hpView;

        public void SetHP(float value = 1f) => _hpView.value = value;

    }
}