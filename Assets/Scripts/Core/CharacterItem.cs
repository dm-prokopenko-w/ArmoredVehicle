using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CharacterItem
    {
        [Min(1)] public float HP = 10;
        [Min(1)] public float Damage = 5;
    }

    public abstract class Character: MonoBehaviour
    {
        [SerializeField] private Collider _col;
        [SerializeField] protected CharacterView _view;

        public float Damage => _damage;
        public Collider Col => _col;

        private float _damage;
        private float _maxHP;
        private float _hp;
        private Action<Collider> _onTrigger;

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
        
        public virtual void ResetGame()
        {
            _hp = _maxHP;
            _view.SetHP();
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
