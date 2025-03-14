using System;
using DataDeclaration;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IOnSceneLoaded
{
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        //TODO: 임시로 추가한 메서드임
        // 나중에 씬 변경 시 할당 해제 기능 추가 필요
        //UIManager.Instance.ChangeUIState(UIType.None);
        
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
                UIManager.ToggleCursor(false);
                break;
        }
    }
}