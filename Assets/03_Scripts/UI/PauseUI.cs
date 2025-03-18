using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;

    private void OnEnable()
    {
        UIManager.ToggleCursor(true);
    }

    private void Start()
    {
        resumeBtn.onClick.AddListener(OnClickResumeButton);
        settingBtn.onClick.AddListener(OnClickSettingButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
        uiType = UIType.Pause;
    }

    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Pause);
    }

    private void OnClickResumeButton()
    {
        UIManager.ToggleCursor(false);
        uiManager.ChangeUIState(UIType.InGame);
    }

    private void OnClickSettingButton()
    {
        uiManager.ChangeUIState(UIType.Setting);
    }

    private void OnClickExitButton()
    {
        GameManager.ExitGame();
    }
}