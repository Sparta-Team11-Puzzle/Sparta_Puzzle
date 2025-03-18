using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class DungeonComplete : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject targetObject;
    private IEventTrigger saveEvent;

    private void Start()
    {
        saveEvent = targetObject.GetComponent<IEventTrigger>();
    }

    public void Interact()
    {
        saveEvent.EventTrigger();
        UIManager.Instance.Fade(0, 1, 3, EndGame);
    }

    private void EndGame()
    {
        GameManager.ChangeScene(SceneType.Ending);
    }
}
