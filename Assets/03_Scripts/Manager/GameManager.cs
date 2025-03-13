using DataDeclaration;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        // 게임 시작 시 마우스 커서 활성화
        UIManager.ToggleCursor(true);
    }

    /// <summary>
    /// 씬 변경 메서드
    /// </summary>
    /// <param name="sceneType">변경하고 싶은 씬 타입</param>
    public static void ChangeScene(SceneType sceneType)
    {
        //TODO: 임시로 추가한 메서드임
        // 나중에 씬 변경 시 할당 해제 기능 추가 필요
        UIManager.Instance.ChangeUIState(UIType.None);

        SceneManager.LoadScene((int)sceneType);
    }
}