using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap playerActionMap;
    public PlayerInput PlayerInput => playerInput;

    public Vector2 MovementInput { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public float MouseZoom { get; private set; }
    public event Action UseTrigger;
    public event Action CameraChangeTrigger;
    public bool InputJump { get; private set; }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        //외부에서 초기화를 실시할 경우 이는 삭제해야함
        Init();
    }

    /// <summary>
    /// C# Invoke 방식으로 사용할 때 인풋 입력을 초기화하는 함수
    /// </summary>
    public void Init()
    {
        //외부에서 PlayerAction을 할당할 경우 초기화하면서 PlayerAction을 확인하도록 함
        if (playerInput.actions != null)
            playerActionMap = playerInput.actions.FindActionMap("Player");
        else
            Debug.LogError("Actions in PlayerInput are Empty.");

        InputAction moveAction = playerActionMap.FindAction("Move");
        moveAction.performed += context => MovementInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MovementInput = Vector2.zero;

        InputAction jumpAction = playerActionMap.FindAction("Jump");
        jumpAction.started += context => InputJump = true;
        jumpAction.canceled += context => InputJump = false;

        InputAction lookAction = playerActionMap.FindAction("Look");
        lookAction.performed += context => MouseDelta = context.ReadValue<Vector2>();
        lookAction.canceled += context => MouseDelta = Vector2.zero;

        InputAction useAction = playerActionMap.FindAction("Use");
        useAction.started += context => UseTrigger?.Invoke();

        InputAction cameraAction = playerActionMap.FindAction("CameraChange");
        cameraAction.started += context => CameraChangeTrigger?.Invoke();
    }

    //Unity Event를 사용할 때 각 이벤트에 할당하는 함수들
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
        {
            InputJump = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            InputJump = false;
        }
            
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
