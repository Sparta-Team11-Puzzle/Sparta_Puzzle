using DataDeclaration;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IOnSceneLoaded
{
    private UIManager uiManager;
    
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 씬 변경 메서드
    /// </summary>
    /// <param name="sceneType">변경하고 싶은 씬 타입</param>
    public static void ChangeScene(SceneType sceneType)
    {
        SceneManager.LoadScene((int)sceneType);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                UIManager.ToggleCursor(true);
                break;
            case 1:
                uiManager.Fade(1, 0, 5);
                UIManager.ToggleCursor(false);
                break;
        }
    }
}