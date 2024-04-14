using System.Collections;
using UnityEngine.UI;
using Game;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace ItemSystem
{
    public class ActivePopupBtn : MonoBehaviour
    {
        [Inject] private ItemController _itemController;

        [SerializeField] private PopupsID _id;
        [SerializeField] private Button _button;
        [SerializeField] private bool _isActive;

        [Inject]
        public void Construct()
        {
            _itemController.AddItemUI(ActivePopupID + _isActive, new Item(_button, _id.ToString()));
        }
    }
}
