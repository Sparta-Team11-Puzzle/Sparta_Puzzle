using System;
using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 환경 설정 클래스
/// </summary>
public class SettingUI : BaseUI
{
    #region Field

    [SerializeField] private Button closeBtn;
    [Space] [SerializeField] private Button generalBtn; // 일반 설정 전환 버튼
    [SerializeField] private Button soundBtn; //소리 설정 전환 버튼
    [SerializeField] private Button keyBtn; //키 설정 전환 버튼
    [Space] [SerializeField] private GameObject generalPanel; // 일반 설정창
    [SerializeField] private GameObject soundPanel; // 소리 설정창
    [SerializeField] private GameObject keyPanel; // 키 설정창
    [Space] [SerializeField] private Button cancelBtn;
    [SerializeField] private Button applyBtn;

    private Dictionary<SettingType, GameObject> settingDict; // 설정 UI 관리용 Dictionary

    private SettingType curSettingType; // 현재 설정 종류
    private ISettingUI curSettingUI;

    private Action cancelBtnAction; // 취소 버튼 클릭 시 실행할 로직
    private Action applyBtnAction; // 적용 버튼 클릭 시 실행할 로직

    #endregion

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
        UIManager.ToggleCursor(true);
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

    #region OnClickMethod
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
    #endregion
    
    /// <summary>
    /// 설정창 및 취소/적용 버튼 기능 변경 메서드
    /// </summary>
    /// <param name="type">활성화할 설정 종류</param>
    private void ToggleSetting(SettingType type)
    {
        curSettingType = type;
        foreach (var setting in settingDict)
        {
            if (curSettingType == setting.Key)
            {
                setting.Value.SetActive(true);
                curSettingUI = setting.Value.GetComponent<ISettingUI>();
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