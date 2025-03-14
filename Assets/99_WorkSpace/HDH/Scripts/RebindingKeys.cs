using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputHandler playerInput;
    [SerializeField] private TextMeshProUGUI bindingKeyName;
    [SerializeField] private GameObject ShowRebind;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void RebindKey()
    {
        ShowRebind.SetActive(true);
        playerInput.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    void RebindComplete()
    {
        rebindingOperation.Dispose();
        playerInput.PlayerInput.SwitchCurrentActionMap("Player");
        ShowRebind.SetActive(false);
    }

}
