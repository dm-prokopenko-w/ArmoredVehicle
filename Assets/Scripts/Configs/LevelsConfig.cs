using System.Collections.Generic;
using Game;
using UnityEngine;

namespace LevelsSystem
{
	[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig", order = 0)]
	public class LevelsConfig : Config
	{
		public List<LevelItemConfig> Levels;
	}
}
