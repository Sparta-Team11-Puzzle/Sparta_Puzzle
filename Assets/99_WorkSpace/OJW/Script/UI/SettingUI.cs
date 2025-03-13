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
        curSettingType = type;
        foreach (var setting in settingDict)
        {
            if (curSettingType == setting.Key)
            {
                setting.Value.SetActive(true);
                curSettingUI =  setting.Value.GetComponent<ISettingUI>();
                cancelBtn.onClick.AddListener(curSettingUI.OnClickCancelButton);
                applyBtn.onClick.AddListener(curSettingUI.OnClickApplyButton);
            }
            else
            {
                setting.Value.SetActive(false);
            }
        }
    }
}
