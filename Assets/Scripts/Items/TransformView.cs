using ItemSystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace LevelsSystem
{
    public class TransformView : MonoBehaviour
    {
        [Inject] private ItemController _itemController;
        
        [SerializeField] private TransformObject _type;

        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(TransformViewID + _type, new Item(transform));
        }
    } 
}
