using DataDeclaration;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 사용자 입력 관리 클래스(싱글톤)
/// </summary>
public class InputManager : Singleton<InputManager>
{
    private InputActionAsset playerInput;  // 플레이어 InputAction 에셋
    
    public float mouseSensitivity;  // 마우스 민감도
    
    public event Action<float> OnChangeMouseSensitivity;

    protected override void Awake()
    {
        base.Awake();
        playerInput = Resources.Load<InputActionAsset>("InputAction/DefaultInputActions");
        
        LoadUserMouseSetting();
        LoadUserKeySetting();
    }

    /// <summary>
    /// 유저 키 세팅 불러오기
    /// </summary>
    public void LoadUserKeySetting()
    {
        string loadDate = PlayerPrefs.GetString(Constant.KEY_MAP, "{}");
        playerInput.LoadBindingOverridesFromJson(loadDate);
    }

    /// <summary>
    /// 유저 키 세팅 저장
    /// </summary>
    public void SaveUserKeySetting()
    {
        string saveData = playerInput.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(Constant.KEY_MAP, saveData);
    }

    /// <summary>
    /// 유저 마우스 세팅 불러오기
    /// </summary>
    public void LoadUserMouseSetting()
    {
        mouseSensitivity = PlayerPrefs.GetFloat(Constant.MOUSE_SENSITIVITY, 0.1f);
    }

    /// <summary>
    /// 유저 마우스 세팅 저장
    /// </summary>
    public void SaveUserMouseSetting()
    {
        PlayerPrefs.SetFloat(Constant.MOUSE_SENSITIVITY, mouseSensitivity);
        OnChangeMouseSensitivity?.Invoke(mouseSensitivity);
    }
}
