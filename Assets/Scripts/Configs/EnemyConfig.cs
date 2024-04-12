using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : Config
    {
        public List<EnemyItem> Enemies;
    }

    [Serializable]
    public class EnemyItem
    {
        public string Id;
        public int HP = 10;
        public int Damage = 5;
        public EnemyView Prefab;
    }
}
