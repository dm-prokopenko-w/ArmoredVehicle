using Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UISystem
{
    public class QuitBtn : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private Button _btn;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.QuitBtnID, new ItemUI(_btn));
        }
    }
}
