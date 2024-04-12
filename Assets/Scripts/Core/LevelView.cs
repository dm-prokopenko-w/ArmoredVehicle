using System;
using UnityEngine;
using UnityEngine.Events;

namespace LevelsSystem
{
    public class LevelView : MonoBehaviour
    {
        public bool IsMoved { get; set; }
        private UnityEvent _onTick = new ();

        private float _endPosY;
        private float _speed;
        private Action<LevelView> _onDespawn;

        private void Start()
        {
            _onTick.AddListener(Move);
        }

        public void Init(float endPosY, UnityEvent onTick, Action<LevelView> onDespawn, float speed)
        {
            _onTick = onTick;
            _endPosY = endPosY;
            IsMoved = true;
            _onDespawn = onDespawn;
            _speed = speed;
        }

        private void Move()
        {
            if(!IsMoved) return;
            
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
           // _image.Rotate(0, 0, 1);

            if (!(_endPosY > transform.position.y)) return;
            IsMoved = false;
            _onDespawn?.Invoke(this);
        }
    }
}