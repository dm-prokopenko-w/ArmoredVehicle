using UISystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace LevelsSystem
{
    public class LevelsParent : MonoBehaviour
    {
        [Inject] private UIController _uiController;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(ParentLevels, new ItemUI(transform));
        }
    } 
}
