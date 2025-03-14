using DataDeclaration;
using UnityEngine;

/// <summary>
/// 일반 환경 설정 클래스
/// </summary>
//TODO: 아직 미구현
public class GeneralSettingUI : MonoBehaviour, ISettingUI
{
    void ISettingUI.OnClickCancelButton()
    {
        UIManager.Instance.PlayButtonSound();
        Debug.Log("일반 설정 취소 버튼");
    }

    void ISettingUI.OnClickApplyButton()
    {
        UIManager.Instance.PlayButtonSound();
        Debug.Log("일반 설정 적용 버튼");
    }
}
