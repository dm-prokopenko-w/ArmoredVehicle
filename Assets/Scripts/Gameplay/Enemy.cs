using Core;
using UnityEngine;
using static Game.Constants;

namespace EnemySystem
{
    public class Enemy : Character
    {
        [SerializeField] protected Animator _anim;
        [SerializeField] protected Rigidbody _rb;

        public bool IsActive { get; private set; }

        private bool _isStartMove = false;
        
        private void Start()
        {
            var rot = UnityEngine.Random.Range(0, 359);
            transform.Rotate(0, rot, 0);
            IsActive = true;
        }

        private void PlayAnim(string id) => _anim.Play(id);

        public override void Dead()
        {
            ActiveEnemy(IsActive = false);
            _isStartMove = false;
            PlayAnim(EnemyIdle);
        }
        
        public override void ResetGame()
        {
            base.ResetGame();
            _isStartMove = false;
            ActiveEnemy(IsActive = true);
        }

        public void StartMove()
        {
            if(_isStartMove) return;
            _isStartMove = true;
            PlayAnim(EnemyRun);
        }
        
        private void ActiveEnemy(bool value)
        {
            Col.enabled = value;
            _view.gameObject.SetActive(value);
        }
    }
}