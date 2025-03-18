using DataDeclaration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 로비 씬 UI 클래스
/// </summary>
public class LobbyUI : BaseUI, IOnSceneLoaded
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;

    private Animator characterAnimator; // 배경에 있는 캐릭터의 애니메이터

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        UIManager.ToggleCursor(true);
    }

    private void Start()
    {
        startBtn.onClick.AddListener(OnClickStartButton);
        settingBtn.onClick.AddListener(OnClickSettingButton);
        exitBtn.onClick.AddListener(OnClickExitButton);

        var character = GameObject.FindGameObjectWithTag("Player");
        characterAnimator = character.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    /// <summary>
    /// 게임 시작 클릭시 실행할 로직
    /// </summary>
    private void OnClickStartButton()
    {
        UIManager.Instance.PlayButtonSound();
        characterAnimator.SetTrigger("IsStart");
    }

    /// <summary>
    /// 환경 설정 클릭시 실행할 로직
    /// </summary>
    private void OnClickSettingButton()
    {
        UIManager.Instance.PlayButtonSound();
        uiManager.ChangeUIState(UIType.Setting);
    }

    /// <summary>
    /// 게임 종료 클릭시 실행할 로직
    /// </summary>
    private void OnClickExitButton()
    {
        GameManager.ExitGame();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != (int)SceneType.Lobby)
        {
            Destroy(gameObject);
        }
    }
}