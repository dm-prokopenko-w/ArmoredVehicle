using Core;
using UnityEngine;
using VContainer;

namespace PlayerSystem
{
    public class Player : Character
    {
        [Inject] private PlayerController _playerController;

        [SerializeField] private Transform GunTr;
        [SerializeField] private Transform TurretTr;
        
        public Vector3 StartBulletPos => GunTr.position;
        private PlayerView _playerView;
        
        [Inject]
        public void Construct()
        {
            _playerView = _view as PlayerView;
            _playerController.InitPlayer(this);
            ActiveGame(false);
        }

        public void ActiveGame(bool value)
        {
            _playerView.ActiveGame(value);
            
            if (value) return;
            ResetGame();
            TurretTr.LookAt(new Vector3(0, 2.9f, -100f));
        }
        
        
        public float Rotate(Vector3 rot)
        {
            TurretTr.Rotate(rot);
            return TurretTr.localEulerAngles.y;
        }

        public Vector3 DirAttack => GunTr.position - TurretTr.position;
        public Quaternion TurretQuat => TurretTr.localRotation;
    }
}