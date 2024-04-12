using Game;
using TMPro;
using UnityEngine;
using VContainer;

namespace UISystem
{
    public class KillsCounter : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private TMP_Text _text;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.KillsCounterID, new ItemUI(_text));
        }
    }
}
