using ItemSystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace LevelsSystem
{
    public class LevelsParent : MonoBehaviour
    {
        [Inject] private ItemController _itemController;
        
        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(ParentLevels, new Item(transform));
        }
    } 
}
