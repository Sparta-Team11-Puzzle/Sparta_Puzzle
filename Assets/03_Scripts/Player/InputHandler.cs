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
    public float MouseZoom { get; private set; }
    public event Action JumpTrigger;
    public event Action UseTrigger;
    public event Action CameraChangeTrigger;

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
            MouseDelta = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            MouseDelta = Vector2.zero;
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            UseTrigger?.Invoke();
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MouseZoom = context.ReadValue<Vector2>().y;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            MouseZoom = 0f;
        }
    }

    public void OnCameraChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            CameraChangeTrigger?.Invoke();
    }

}
