using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;
    
    private Animator characterAnimator;

    [SerializeField] private Transform destination;

    private void Start()
    {
        startBtn.onClick.AddListener(OnClickStartButton);
        settingBtn.onClick.AddListener(OnClickSettingButton);
        exitBtn.onClick.AddListener(OnClickExitButton);

        var character = GameObject.FindGameObjectWithTag("Player");
        characterAnimator = character.GetComponent<Animator>();
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
        uiType = UIType.Lobby;
    }

    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Lobby);
    }

    private void OnClickStartButton()
    {
        UIManager.Instance.PlayButtonSound();
        characterAnimator.SetTrigger("IsStart");
        //StartCoroutine(UIManager.Instance.Fade(0, 1, 2, () => GameManager.ChangeScene(SceneType.Main)));
    }

    private void OnClickSettingButton()
    {
        UIManager.Instance.PlayButtonSound();
        uiManager.ChangeUIState(UIType.Setting);
    }

    private void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}