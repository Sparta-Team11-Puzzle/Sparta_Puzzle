using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private bool isCursorOn;

    public List<BaseUI> UIList { get; private set; }
    public LobbyUI LobbyUI { get; private set; }
    public SettingUI SettingUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        UIList = new List<BaseUI>();
        
        LobbyUI = InitUI<LobbyUI>();
        SettingUI = InitUI<SettingUI>();
    }

    /// <summary>
    /// 마우스 커서 On/Off 기능
    /// </summary>
    /// <param name="isActive">True: 마우스활성화 False: 마우스 비활성화</param>
    public static void ToggleCursor(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ChangeUIState(UIType type)
    {
        foreach (var ui in UIList)
        {
            ui.ActiveUI(type);
        }
    }

    private T InitUI<T>() where T : BaseUI
    {
        var ui = GetComponentInChildren<T>(true);
        if (ui == null)
        {
            var go = new GameObject(nameof(T));
            go.transform.SetParent(transform);
            ui = go.AddComponent<T>();
            
        }
        ui.Init(this);
        return ui;
    }
}
