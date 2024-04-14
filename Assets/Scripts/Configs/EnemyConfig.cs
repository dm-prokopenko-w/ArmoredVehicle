using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;
using static Game.Constants;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : Config
    {
        public List<EnemyItem> Enemies;
    }

    [Serializable]
    public class EnemyItem: CharacterItem
    {
        public EnemyTypes Id;
        public EnemyView Prefab;
    }
}
