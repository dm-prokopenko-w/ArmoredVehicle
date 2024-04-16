using Core;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyView : CharacterView
    {
        [SerializeField] private Transform _canvas;
        private Transform _cam;

        private void Start()
        {
            _cam = Camera.main.transform;
        }

        private void Update()
        {
            _canvas.LookAt(_cam);
        }
    }
}
