using System;
using Game;
using UnityEngine;
using UnityEngine.Events;

namespace LevelsSystem
{
    public class LevelView : MonoBehaviour
    {
        private Vector3 _startPos;
        private float _speed;
        private Action _onDespawn;
        
        public void Move()
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed);

            if (transform.position.z < -Constants.LevelStep)
            {
                Debug.LogError(111);
                _onDespawn?.Invoke();
            }
        }

        public void Init(float speed, Action onDespawn)
        {
            _onDespawn = onDespawn;
            _speed = speed;
            _startPos = transform.position;
        } 

        public void ResetGame() => transform.position = _startPos;
    }
}