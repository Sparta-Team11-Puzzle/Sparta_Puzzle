using DataDeclaration;
using UnityEngine;

public class GeneralSettingUI : MonoBehaviour, ISettingUI
{
    void ISettingUI.OnClickCancelButton()
    {
        Debug.Log("일반 설정 취소 버튼");
    }

    void ISettingUI.OnClickApplyButton()
    {
        Debug.Log("일반 설정 적용 버튼");
    }
}
