using System;
using Game;
using UnityEngine;
using VContainer;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;
        
        public Action OnСollision;

        public float PosY => transform.position.x;
        
        [Inject]
        public void Construct()
        {
            _playerController.InitPlayer(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.tag.Equals(Constants.AsteroidTag))return;
            
            OnСollision?.Invoke();
        }
    }
}