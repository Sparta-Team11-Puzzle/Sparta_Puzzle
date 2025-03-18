using DataDeclaration;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput; //PlayerInput 컴포넌트
    private InputActionMap playerActionMap; //InputAction에서 플레이어 조작을 담당하는 ActionMap
    public PlayerInput PlayerInput => playerInput;

    public Vector2 MovementInput { get; private set; } //PlayerController에 이동 조작 입력값을 전달
    public Vector2 MouseDelta { get; private set; } //PlayerController에 마우스 이동 입력값을 전달
    public float MouseZoom { get; private set; } //PlayerController에 카메라 거리 조절 값을 전달
    public event Action UseTrigger; //Use 입력시 실행하는 델리게이트
    public event Action CameraChangeTrigger; //CameraChange 입력시 실행하는 델리게이트
    public bool InputJump { get; private set; } //점프 입력 여부를 전달

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

        //이동 키 입력 값을 MovementInput에 저장 / 값이 없을 경우 0 벡터를 저장
        InputAction moveAction = playerActionMap.FindAction("Move");
        moveAction.performed += context => MovementInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MovementInput = Vector2.zero;

        //키 입력시 InputJump에 true를 저장 / 입력이 없을 때는 false
        InputAction jumpAction = playerActionMap.FindAction("Jump");
        jumpAction.started += context => InputJump = true;
        jumpAction.canceled += context => InputJump = false;

        //마우스 이동 값을 MovementInput에 저장 / 값이 없을 경우 0 벡터를 저장
        InputAction lookAction = playerActionMap.FindAction("Look");
        lookAction.performed += context => MouseDelta = context.ReadValue<Vector2>();
        lookAction.canceled += context => MouseDelta = Vector2.zero;

        //키 입력시 UseTrigger를 실행
        InputAction useAction = playerActionMap.FindAction("Use");
        useAction.started += context => UseTrigger?.Invoke();

        //키 입력시 CameraChangeTrigger를 실행
        InputAction cameraAction = playerActionMap.FindAction("CameraChange");
        cameraAction.started += context => CameraChangeTrigger?.Invoke();
        
        //키 입력시 OnPause 함수를 실행
        InputAction pauseAction = playerActionMap.FindAction("Pause");
        pauseAction.started += context => OnPause(context);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //입력시 Pause UI를 활성화
            UIManager.Instance.ChangeUIState(UIType.Pause);
        }
    }
}
