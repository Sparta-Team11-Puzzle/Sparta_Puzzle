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
        // 유저 마우스 감도 세팅값으로 초기화
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat(Constant.MOUSE_SENSITIVITY);
    }

    private void Start()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
    }

    /// <summary>
    /// 마우스 감도 슬라이더에 할당할 메서드
    /// </summary>
    /// <param name="value">Slider.value</param>
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
