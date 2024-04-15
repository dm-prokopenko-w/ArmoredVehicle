using System.Collections.Generic;
using Game;
using UnityEngine;

namespace LevelsSystem
{
    [CreateAssetMenu(fileName = "LevelItemConfig", menuName = "Configs/LevelItemConfig", order = 0)]

    public class LevelItemConfig : Config
    {
        [Range(0, 99)]public int MinCountEnemy = 5;
        [Range(0, 100)]public int MaxCountEnemy = 10;
        public float LevelSpeed;
        [Min(2)] public int CountRandomLevels;
        public LevelView StartLevelViews;
        public List<LevelView> TypesLevelViews;
        public LevelView FinishLevelViews;

        private void OnValidate()
        {
            if (MinCountEnemy >= MaxCountEnemy)
            {
                MaxCountEnemy = MinCountEnemy + 1;
            }
        }
    }
}