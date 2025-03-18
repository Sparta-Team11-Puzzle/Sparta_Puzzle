using DataDeclaration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 키 세팅 환경 설정 클래스
/// </summary>
public class KeySettingUI : MonoBehaviour, ISettingUI
{
    private InputManager inputManager;

    [SerializeField] private Slider mouseSensitivitySlider; // 마우스 민감도 조절 슬라이드
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    private KeyBindingUI[] keyBindingUIs;

    private void Awake()
    {
        keyBindingUIs = GetComponentsInChildren<KeyBindingUI>(true);

        mouseSensitivitySlider.minValue = 0.1f;
        mouseSensitivitySlider.maxValue = 3f;
    }

    private void OnEnable()
    {
        if (inputManager != null)
        {
            InitUI();
        }
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        InitUI();
        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
    }

    /// <summary>
    /// 마우스 감도 슬라이더에 할당할 메서드
    /// </summary>
    /// <param name="value">Slider.value</param>
    private void OnMouseSensitivityChanged(float value)
    {
        inputManager.mouseSensitivity = value;
        mouseSensitivityText.text = value.ToString("N2");
    }

    /// <summary>
    /// UI 변경 전 상태로 갱신
    /// </summary>
    private void InitUI()
    {
        inputManager.LoadUserMouseSetting();
        mouseSensitivitySlider.value = inputManager.mouseSensitivity;
        mouseSensitivityText.text = mouseSensitivitySlider.value.ToString("N2");

        inputManager.LoadUserKeySetting();

        foreach (KeyBindingUI keyBindingUI in keyBindingUIs)
        {
            keyBindingUI.UpdateUI();
        }
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
    }

    void ISettingUI.OnClickApplyButton()
    {
        UIManager.Instance.PlayButtonSound();
        inputManager.SaveUserMouseSetting();
        inputManager.SaveUserKeySetting();
    }
}