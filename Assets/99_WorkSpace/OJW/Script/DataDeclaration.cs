namespace DataDeclaration
{
    public enum SceneType
    {
        Lobby,
        Main,
    }
    
    public enum UIType
    {
        None,
        Lobby,
        Setting,
    }

    public enum SettingType
    {
        General,
        Sound,
        Key,
    }

    public static class Constant
    {
        public const string MASTER_VOL = "masterVol";
        public const string BGM_VOL = "bgmVol";
        public const string SFX_VOL = "sfxVol";
    }
}