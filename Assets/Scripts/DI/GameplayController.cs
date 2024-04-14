using System;
using Game;
using ItemSystem;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem
{
    public class GameplayController: IStartable
    {
        [Inject] private ItemController _itemController;

        public Action<bool> OsPlayGame;
        public Action OnGameOver;
        public Action OnResetGame;

        private bool _isPlayGame = false;
        private int _countAsteroidCollision = 0;
        
        public void Start()
        {
            _itemController.SetAction(Constants.GameBtnID, UpdateGame);
            _itemController.SetText(Constants.KillsCounterID, "0");
        }

        private void UpdateGame()
        {
            _isPlayGame = !_isPlayGame;
            OsPlayGame?.Invoke(_isPlayGame);

            if (_isPlayGame)
            {
                _itemController.SetActivBtn(Constants.GameBtnID, false);
            }
        }

        public void ResetGame()
        {
            _itemController.SetActivBtn(Constants.GameBtnID, true);
            _countAsteroidCollision = 0;
            _itemController.SetText(Constants.KillsCounterID, _countAsteroidCollision.ToString());
            OnResetGame?.Invoke();
        }
        
        public void GameOver()
        {
            _countAsteroidCollision++;
            _itemController.SetText(Constants.KillsCounterID, _countAsteroidCollision.ToString());
            OnGameOver?.Invoke();
        }
    }
}
