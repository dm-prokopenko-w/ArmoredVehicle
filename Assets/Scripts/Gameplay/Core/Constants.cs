namespace Game
{
    public static class Constants
    {
        public const string EnemyConfigPath = "EnemyConfig";
        public const string PlayerConfigPath = "PlayerConfig";
        public const string LevelsConfigPath = "LevelsConfig";

        public const string BalletParentID = "BalletParent";
        public const string CameraAnimatorID = "CameraAnimator";
        public const string ActivePopupID = "ActivePopup";
        
        public const string TextViewID = "TextViewID";
        public const string ButtonViewID = "ButtonViewID";
        public const string TransformViewID = "TransformViewID";

        public const string ShowKey = "Show";
        public const string HideKey = "Hide";

        public const string StartStateCamera = "StartState";
        public const string GameStateCamera = "GameState";

        public const string EnemyIdle = "Idle";
        public const string EnemyRun = "Run";

        public const string KillsCountText = "Kill: ";
        public const string LevelsCountText = "Level: ";

        public const int DistEnemyMoveToPlayer = 50;
        public const int LevelStep = 105;
        public const int SpeedBullet = 500;
        public const float SpeedEnemy = 0.6f;
        public const int DistStopMove = -12;
        public const int DistDead = -18;
        public const float SpawnSize = 35f;
        
        public enum PopupsID
        {
            None,
            Win,
            Lose,
        }

        public enum TransformObject
        {
            None,
            
            ActiveLevelsParent,
            InactiveLevelsParent,

            ActiveBulletParent,
            InactiveBulletParent,
            
            ActiveEnemyParent,
            InactiveEnemyParent,
        }

        public enum ButtonObject
        {
            None,
            StartGame,
        }
        
        public enum TextObject
        {
            None,
            KillCounter,
            LvlCounter,
        }
    }
}