using DataDeclaration;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActionAsset playerInput;  // 플레이어 InputAction 에셋
    
    public float mouseSensitivity;
    
    public InputActionAsset PlayerInput => playerInput;

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
        string loadDate = PlayerPrefs.GetString("bindingOverrides", "{}");
        playerInput.LoadBindingOverridesFromJson(loadDate);
        
    }

    /// <summary>
    /// 유저 키 세팅 저장
    /// </summary>
    public void SaveUserKeySetting()
    {
        PlayerPrefs.GetFloat(Constant.MOUSE_SENSITIVITY, 0.1f);
        
        string saveData = playerInput.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("bindingOverrides", saveData);
    }

    public void LoadUserMouseSetting()
    {
        mouseSensitivity = PlayerPrefs.GetFloat(Constant.MOUSE_SENSITIVITY, 0.1f);
    }

    public void SaveUserMouseSetting()
    {
        PlayerPrefs.SetFloat(Constant.MOUSE_SENSITIVITY, mouseSensitivity);
    }
}
