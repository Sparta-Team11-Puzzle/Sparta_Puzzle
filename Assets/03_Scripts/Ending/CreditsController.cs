using DataDeclaration;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    //애니메이션의 특정 지점에서 페이드 아웃 효과를 실행->완료되면 로비 씬으로 이동
    public void ActiveEndingCreditEffect()
    {
        UIManager.Instance.Fade(0f, 1f, 10f, LoadLobbyScene);
    }

    void LoadLobbyScene()
    {
        GameManager.ChangeScene(SceneType.Lobby);
    }
}
