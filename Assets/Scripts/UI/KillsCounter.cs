using Game;
using TMPro;
using UnityEngine;
using VContainer;

namespace ItemSystem
{
    public class KillsCounter : MonoBehaviour
    {
        [Inject] private ItemController _uiController;

        [SerializeField] private TMP_Text _text;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.KillsCounterID, new Item(_text));
        }
    }
}
