using DataDeclaration;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        UIManager.ToggleCursor(true);
    }

    public static void ChangeScene(SceneType sceneType)
    {
        SceneManager.LoadScene((int)sceneType);
    }
}
