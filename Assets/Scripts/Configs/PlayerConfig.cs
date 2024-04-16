using Game;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : Config
    {
        [Min(1)] public int HP = 100;
        [Min(1)] public int Damage = 3;
        public Bullet Bullet;
    }
}