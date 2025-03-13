using System;
using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private Button closeBtn;
    [Space]
    [SerializeField] private Button generalBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button keyBtn;
    [Space]
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject keyPanel;
    [Space]
    [SerializeField] private Button cancelBtn;
    [SerializeField] private Button applyBtn;
    
    private Dictionary<SettingType, GameObject> settingDict;

    private SettingType curSettingType;
    private ISettingUI curSettingUI;
    
    private Action cancelBtnAction;
    private Action applyBtnAction;

    private void Awake()
    {
        uiType = UIType.Setting;
        
        settingDict = new Dictionary<SettingType, GameObject>
        {
            { SettingType.General, generalPanel },
            { SettingType.Sound, soundPanel },
            { SettingType.Key, keyPanel }
        };
    }

    private void OnEnable()
    {
        ToggleSetting(SettingType.General);
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(OnCloseButtonClick);
        
        generalBtn.onClick.AddListener(OnGeneralButtonClick);
        soundBtn.onClick.AddListener(OnSoundButtonClick);
        keyBtn.onClick.AddListener(OnKeyButtonClick);
        
        cancelBtn.onClick.AddListener(() => cancelBtnAction?.Invoke());
        applyBtn.onClick.AddListener(() => applyBtnAction?.Invoke());
    }
    
    public override void Init(UIManager manager)
    {
        base.Init(manager);
        uiType = UIType.Setting;
    }

    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Setting);
    }

    private void OnCloseButtonClick()
    {
        uiManager.PlayButtonSound();
        uiManager.ChangeUIState(UIType.Lobby);
    }
    
    private void OnGeneralButtonClick()
    {
        uiManager.PlayButtonSound();
        ToggleSetting(SettingType.General);
    }

    private void OnSoundButtonClick()
    {
        uiManager.PlayButtonSound();
        ToggleSetting(SettingType.Sound);
    }

    private void OnKeyButtonClick()
    {
        uiManager.PlayButtonSound();
        ToggleSetting(SettingType.Key);
    }

    private void ToggleSetting(SettingType type)
    {
        curSettingType = type;
        foreach (var setting in settingDict)
        {
            if (curSettingType == setting.Key)
            {
                setting.Value.SetActive(true);
                curSettingUI =  setting.Value.GetComponent<ISettingUI>();
                cancelBtnAction = curSettingUI.OnClickCancelButton;
                applyBtnAction = curSettingUI.OnClickApplyButton;
            }
            else
            {
                setting.Value.SetActive(false);
            }
        }
    }
}
