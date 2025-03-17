using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DungeonComplete : MonoBehaviour, IInteractable
{
    private string sceneNama = "00_Lobby";

    public void Interact()
    {
        UIManager.Instance.Fade(0, 1, 3, EndGame);
    }

    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(sceneNama);
    }
}
