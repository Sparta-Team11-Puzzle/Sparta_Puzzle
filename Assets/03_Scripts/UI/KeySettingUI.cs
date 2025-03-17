using DataDeclaration;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 키 세팅 환경 설정 클래스
/// </summary>
public class KeySettingUI : MonoBehaviour, ISettingUI
{
    private InputManager inputManager;

    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    private KeyBindingUI[] keyBindingUIs;

    private void Awake()
    {
        keyBindingUIs = GetComponentsInChildren<KeyBindingUI>(true);
    }

    private void OnEnable()
    {
        if (inputManager != null)
        {
            inputManager.LoadUserMouseSetting();
            mouseSensitivitySlider.value = inputManager.mouseSensitivity;
            mouseSensitivityText.text = (mouseSensitivitySlider.value * 100f).ToString("N0");
            
            inputManager.LoadUserKeySetting();
            foreach (KeyBindingUI keyBindingUI in keyBindingUIs)
            {
                keyBindingUI.UpdateUI();
            }
        }
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
    }

    /// <summary>
    /// 마우스 감도 슬라이더에 할당할 메서드
    /// </summary>
    /// <param name="value">Slider.value</param>
    private void OnMouseSensitivityChanged(float value)
    {
        inputManager.mouseSensitivity = value;
        mouseSensitivityText.text = (value * 100f).ToString("N0");
    }

    void ISettingUI.OnClickCancelButton()
    {
        UIManager.Instance.PlayButtonSound();
        
        inputManager.LoadUserMouseSetting();
        mouseSensitivitySlider.value = inputManager.mouseSensitivity;
        
        inputManager.LoadUserKeySetting();
        foreach (KeyBindingUI keyBindingUI in keyBindingUIs)
        {
            keyBindingUI.UpdateUI();
        }
        
        Debug.Log("키 설정 취소 버튼");
    }

    void ISettingUI.OnClickApplyButton()
    {
        UIManager.Instance.PlayButtonSound();
        inputManager.SaveUserMouseSetting();
        inputManager.SaveUserKeySetting();
        Debug.Log("키 설정 적용 버튼");
    }
}