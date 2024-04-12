using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : Config
    {
        public int HP = 100;
        public int Damage = 3;
        public GameObject Bullet;
    }
}