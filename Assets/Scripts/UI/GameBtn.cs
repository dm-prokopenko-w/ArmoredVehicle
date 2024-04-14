using Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ItemSystem
{
    public class GameBtn : MonoBehaviour
    {
        [Inject] private ItemController _uiController;

        [SerializeField] private Button _btn;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.GameBtnID, new Item(_btn));
        }
    }
}