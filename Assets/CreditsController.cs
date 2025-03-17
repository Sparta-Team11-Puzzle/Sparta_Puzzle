using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playTimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActiveEndingCreditEffect()
    {
        UIManager.Instance.Fade(1f, 0f, 2f, LoadLobbyScene);
    }

    void LoadLobbyScene()
    {
        GameManager.ChangeScene(SceneType.Lobby);
    }


}
