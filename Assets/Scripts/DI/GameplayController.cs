using System;
using ItemSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static Game.Constants;

namespace GameplaySystem
{
    public class GameplayController: IStartable
    {
        [Inject] private ItemController _itemController;
        [Inject] private PopupController _popupController;

        public Action<bool> OsPlayGame;
        public Action OnGameOver;
        public Action OnGameWin;
        public Action OnResetGame;

        private bool _isPlayGame = false;
        private Animator _animCamera;

        public void Start()
        {
            _itemController.SetAction(ActivePopupID + false, (id) => ResetGame());
            _itemController.SetAction(GameBtnID, UpdateGame);
            _itemController.SetText(KillsCounterID, "0");
            _itemController.PlayAnim(CameraAnimatorID, StartStateCamera);
        }

        private void UpdateGame()
        {
            _isPlayGame = !_isPlayGame;
            OsPlayGame?.Invoke(_isPlayGame);

            if (_isPlayGame)
            {
                _itemController.SetActivBtn(GameBtnID, false);
                _itemController.PlayAnim(CameraAnimatorID, GameStateCamera);
            }
            else
            {
                _itemController.PlayAnim(CameraAnimatorID, StartStateCamera);
            }
        }

        public void ResetGame()
        {
            if (_isPlayGame) return;

            _itemController.SetActivBtn(GameBtnID, true);
            OnResetGame?.Invoke();
            UpdateGame();
        }
        
        public void GameOver()
        {           
            UpdateGame();
            _popupController.ShowPopup(PopupsID.Lose.ToString());
            OnGameOver?.Invoke();
        }
        
        public void GameWin()
        {           
            UpdateGame();
            _popupController.ShowPopup(PopupsID.Win.ToString());
            OnGameWin?.Invoke();
        }
    }
}
