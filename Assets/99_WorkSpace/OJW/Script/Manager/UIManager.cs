using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private bool isCursorOn;

    /// <summary>
    /// 마우스 커서 On/Off 기능
    /// </summary>
    /// <param name="isActive">True: 마우스활성화 False: 마우스 비활성화</param>
    public static void ToggleCursor(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
