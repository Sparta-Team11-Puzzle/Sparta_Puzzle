using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    private InputHandler playerInput;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public event Action OnComplet;

    private void Start()
    {
        playerInput = CharacterManager.Instance.Player.GetComponent<InputHandler>();
    }

    public void RebindKey(InputActionReference actionReference)
    {
        playerInput.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = actionReference.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    void RebindComplete()
    {
        rebindingOperation.Dispose();
        playerInput.PlayerInput.SwitchCurrentActionMap("Player");
        OnComplet?.Invoke();
    }

    void Init()
    {
        //playerInput.PlayerInput.actions;
    }

}
