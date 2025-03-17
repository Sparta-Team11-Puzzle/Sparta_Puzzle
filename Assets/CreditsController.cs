using DataDeclaration;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.ToggleCursor(false);
    }

    public void ActiveEndingCreditEffect()
    {
        UIManager.Instance.Fade(0f, 1f, 10f, LoadLobbyScene);
    }

    void LoadLobbyScene()
    {
        GameManager.ChangeScene(SceneType.Lobby);
        UIManager.ToggleCursor(true);
    }
}
