namespace Game
{
    public static class Constants
    {
        public const string LevelsConfigPath = "LevelsConfig";

        public const string ParentLevels = "ParentLevels";
        public const string AsteroidTag = "Asteroid";

        public const string AnimIdDied = "Died";

        public const string GameBtnID = "GameBtnID";
        public const string ResetBtnID = "ResetBtnID";
        public const string QuitBtnID = "QuitBtnID";
        
        public const string KillsCounterID = "KillsCounter";

        public const string ShowKey = "Show";
        public const string HideKey = "Hide";

        public const string ActivePopupID = "ActivePopup";

        public const int LevelStep = 105;
        
        public enum PopupsID
        {
            None,
            Win,
            Lose
        }
    }
}