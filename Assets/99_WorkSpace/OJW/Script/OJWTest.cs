using UnityEngine;
using UnityEngine.InputSystem;

public class OJWTest : MonoBehaviour
{
    private InputManager inputManager;
    
    private  PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        //playerInput.actions = InputManager.Instance.PlayerInput;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("이동 키 입력");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("점프 키 입력");
        }
    }
}