using Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UISystem
{
    public class GameBtn : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private Button _btn;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.GameBtnID, new ItemUI(_btn));
        }
    }
}