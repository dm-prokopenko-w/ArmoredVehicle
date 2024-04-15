using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using VContainer;

namespace ItemSystem
{
    public class CameraAnimator : MonoBehaviour
    {
        [Inject] private ItemController _uiController;

        [SerializeField] private Animator _anim;
        
        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(Constants.CameraAnimatorID, new Item(_anim));
        }
    }
}
