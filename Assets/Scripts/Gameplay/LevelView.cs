using System;
using Game;
using UnityEngine;

namespace LevelsSystem
{
    public class LevelView : MonoBehaviour
    {
        private float _speed;
        private Action _onDespawn;
        
        public void Move()
        {
            if (transform.position.z <= -Constants.LevelStep)
            {
                _onDespawn?.Invoke();
            }
        }

        public void Init(Action onDespawn)
        {
            _onDespawn = onDespawn;
        }
    }
}