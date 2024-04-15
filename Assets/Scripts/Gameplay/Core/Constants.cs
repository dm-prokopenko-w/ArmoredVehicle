namespace Game
{
    public static class Constants
    {
        public const string EnemyConfigPath = "EnemyConfig";
        public const string PlayerConfigPath = "PlayerConfig";
        public const string LevelsConfigPath = "LevelsConfig";

        public const string ParentLevels = "ParentLevels";
        public const string ParentEnemy = "ParentEnemy";

        public const string AnimIdDied = "Died";

        public const string GameBtnID = "GameBtnID";
        public const string ResetBtnID = "ResetBtnID";
        public const string QuitBtnID = "QuitBtnID";
        public const string BalletParentID = "BalletParent";
        public const string CameraAnimatorID = "CameraAnimator";

        public const string KillsCounterID = "KillsCounter";

        public const string ShowKey = "Show";
        public const string HideKey = "Hide";

        public const string StartStateCamera = "StartState";
        public const string GameStateCamera = "GameState";

        public const string EnemyIdle = "Idle";
        public const string EnemyRun = "Run";

        public const string ActivePopupID = "ActivePopup";
        
        public const string PopupLoseText = "";
        public const string PopupWinText = "You win";


        public const int DistEnemyMoveToPlayer = 50;
        public const int LevelStep = 105;
        public const int SpeedBullet = 500;
        public const float SpeedEnemy = 0.4f;
        public const int DistStopMove = -12;
        public const int DistDead = -18;
        public const float SpawnSize = 40f;
        
        public enum PopupsID
        {
            None,
            Win,
            Lose
        }

        public enum ObjectState
        {
            None,
            Active,
            Inactive
        }

        public enum EnemyTypes
        {
            Simple,
        }
    }
}