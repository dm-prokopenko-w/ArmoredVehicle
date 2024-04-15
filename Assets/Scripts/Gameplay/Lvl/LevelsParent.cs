using ItemSystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace LevelsSystem
{
    public class LevelsParent : MonoBehaviour
    {
        [Inject] private ItemController _itemController;
        
        [SerializeField] private ObjectState _state;

        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(ParentLevels + _state, new Item(transform));
        }
    } 
}
