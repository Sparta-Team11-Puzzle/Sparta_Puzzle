using DataDeclaration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeySettingUI : MonoBehaviour, ISettingUI
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI  mouseSensitivityText;

    private void OnEnable()
    {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat(Constant.MOUSE_SENSITIVITY);
    }

    private void Start()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
    }

    private void OnMouseSensitivityChanged(float value)
    {
        mouseSensitivityText.text = (value * 100f).ToString("N0");
    }

    void ISettingUI.OnClickCancelButton()
    {
        UIManager.Instance.PlayButtonSound();
        Debug.Log("키 설정 취소 버튼");
    }

    void ISettingUI.OnClickApplyButton()
    {
        UIManager.Instance.PlayButtonSound();
        Debug.Log("키 설정 적용 버튼");
    }
}
