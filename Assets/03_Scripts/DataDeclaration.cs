using UnityEngine.SceneManagement;

// 데이터 선언부
namespace DataDeclaration
{
    #region Enum
    /// <summary>
    /// 씬 종류
    /// <para>빌드 세팅 인덱스에 맞게 순서 설정 필수</para>
    /// </summary>
    public enum SceneType
    {
        Lobby,
        Main,
        Ending
    }
    
    /// <summary>
    /// BaseUI를 상속 받은 UI 종류
    /// </summary>
    public enum UIType
    {
        None,
        Lobby,
        Setting,
        InGame,
        Pause,
    }

    /// <summary>
    /// 환경 설정 종류
    /// </summary>
    public enum SettingType
    {
        General,
        Sound,
        Key,
    }
    #endregion

    #region Interface
    /// <summary>
    /// 환경 설정 취소/적용 버튼 기능
    /// </summary>
    public interface ISettingUI
    {
        public void OnClickCancelButton();
        public void OnClickApplyButton();
    }

    /// <summary>
    /// 씬 전환 완료 후 로직 실행 기능
    /// <para>등록: SceneManager.sceneLoaded += OnSceneLoaded</para>
    /// <para>해제: SceneManager.sceneLoaded -= OnSceneLoaded</para>
    /// </summary>
    public interface IOnSceneLoaded
    {
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode);
    }
    #endregion
    
    /// <summary>
    /// 상수 선언 클래스
    /// </summary>
    public static class Constant
    {
        public const string MASTER_VOL = "masterVol";
        public const string BGM_VOL = "bgmVol";
        public const string SFX_VOL = "sfxVol";
        
        public const string MOUSE_SENSITIVITY = "mouseSensitivity";
    }
}