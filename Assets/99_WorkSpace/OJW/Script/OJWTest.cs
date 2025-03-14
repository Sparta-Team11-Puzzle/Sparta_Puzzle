using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OJWTest : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.Test();
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