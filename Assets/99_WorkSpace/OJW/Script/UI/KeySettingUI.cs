using DataDeclaration;
using UnityEngine;

public class KeySettingUI : MonoBehaviour, ISettingUI
{
    void ISettingUI.OnClickCancelButton()
    {
        Debug.Log("키 설정 취소 버튼");
    }

    void ISettingUI.OnClickApplyButton()
    {
        Debug.Log("키 설정 적용 버튼");
    }
}
