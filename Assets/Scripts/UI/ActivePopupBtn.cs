using System.Collections;
using UnityEngine.UI;
using Game;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace UISystem
{
    public class ActivePopupBtn : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private PopupsID _id;
        [SerializeField] private Button _button;
        [SerializeField] private bool _isActive;

        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(ActivePopupID + _isActive, new ItemUI(_button, _id.ToString()));
        }
    }
}
