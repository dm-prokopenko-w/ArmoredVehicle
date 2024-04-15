using ItemSystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace PlayerSystem
{
    public class BulletParent : MonoBehaviour
    {
        [Inject] private ItemController _itemController;

        [SerializeField] private ObjectState _state;

        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(BalletParentID + _state, new Item(transform));
        }
    }
}