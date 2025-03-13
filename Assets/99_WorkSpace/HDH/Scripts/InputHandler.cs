using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public bool IsRun { get; private set; }
    public bool IsUse { get; private set; }

    public event Action JumpTrigger;

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            MovementInput = context.ReadValue<Vector2>();
        else if(context.phase == InputActionPhase.Canceled)
            MovementInput = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpTrigger?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            MouseDelta = context.ReadValue<Vector2>().normalized;
        else if (context.phase == InputActionPhase.Canceled)
            MouseDelta = Vector2.zero;
    }
}
