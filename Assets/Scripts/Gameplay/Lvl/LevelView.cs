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
        
        private Vector3 _startPos;
        
        public List<Enemy> Init()
        {
            _startPos = transform.position;
            return _enemies;
        }

        public void ResetGame()
        {
            transform.position = _startPos;
        }
    }
}