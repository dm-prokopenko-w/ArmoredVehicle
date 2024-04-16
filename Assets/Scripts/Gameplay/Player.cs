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
        [SerializeField] private GameObject Line;
        [SerializeField] private GameObject UI;
        
        public Vector3 StartBulletPos => GunTr.position;

        [Inject]
        public void Construct()
        {
            _playerController.InitPlayer(this);
            ActiveGame(false);
        }

        public void ActiveGame(bool value)
        {
            Line.SetActive(value);
            UI.SetActive(value);
            
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