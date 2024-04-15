namespace Game
{
    public static class Constants
    {
        public const string EnemyConfigPath = "EnemyConfig";
        public const string PlayerConfigPath = "PlayerConfig";
        public const string LevelsConfigPath = "LevelsConfig";

        public const string ParentLevels = "ParentLevels";
        public const string AsteroidTag = "Asteroid";

        public const string AnimIdDied = "Died";

        public const string GameBtnID = "GameBtnID";
        public const string ResetBtnID = "ResetBtnID";
        public const string QuitBtnID = "QuitBtnID";
        public const string BalletParentID = "BalletParent";

        public const string KillsCounterID = "KillsCounter";

        public const string ShowKey = "Show";
        public const string HideKey = "Hide";

        public const string ActivePopupID = "ActivePopup";


        public const int LevelStep = 105;
        public const int SpeedBullet = 500;

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