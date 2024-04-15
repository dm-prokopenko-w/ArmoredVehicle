using System;
using Core;
using Game;
using UnityEngine;
using VContainer;

namespace PlayerSystem
{
    public class Player : Character
    {
        [Inject] private PlayerController _playerController;

        [SerializeField] private Transform GunTr;
        [SerializeField] private Transform TurretTr;

        public Action OnСollision;

        public float PosY => transform.position.x;
        public Vector3 StartBulletPos => GunTr.position;

        [Inject]
        public void Construct()
        {
            _playerController.InitPlayer(this);
        }

        public float Rotate(Vector3 rot)
        {
            TurretTr.Rotate(rot);
            return TurretTr.localEulerAngles.y;
        }

        public Vector3 DirAttack => GunTr.position - TurretTr.position;
    }
}