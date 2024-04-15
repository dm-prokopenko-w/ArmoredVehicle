using ItemSystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace LevelsSystem
{
    public class EnemyParent : MonoBehaviour
    {
        [Inject] private ItemController _itemController;
        [SerializeField] private ObjectState _state;

        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(ParentEnemy + _state, new Item(transform));
        }
    } 
}
