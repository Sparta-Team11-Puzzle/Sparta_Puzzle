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
        //TODO: 임시로 추가한 메서드임
        UIManager.Instance.ChangeUIState(UIType.None);
        
        SceneManager.LoadScene((int)sceneType);
    }
}
