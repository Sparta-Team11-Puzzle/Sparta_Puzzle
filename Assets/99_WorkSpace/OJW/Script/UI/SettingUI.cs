using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private Button closeBtn;
    
    [SerializeField] private Button generalBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button keyBtn;
    
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject keyPanel;
    
    private Dictionary<SettingType, GameObject> settingDict;

    private SettingType curSetting;

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
        closeBtn.onClick.AddListener(() => uiManager.ChangeUIState(UIType.Lobby));
        
        generalBtn.onClick.AddListener(() => ToggleSetting(SettingType.General));
        soundBtn.onClick.AddListener(() => ToggleSetting(SettingType.Sound));
        keyBtn.onClick.AddListener(() => ToggleSetting(SettingType.Key));
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

    private void ToggleSetting(SettingType type)
    {
        curSetting = type;
        foreach (var setting in settingDict)
        {
            setting.Value.SetActive(curSetting == setting.Key);
        }
    }
}
