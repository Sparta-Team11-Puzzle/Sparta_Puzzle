using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(OnClickStartButton);
        settingBtn.onClick.AddListener(() => uiManager.ChangeUIState(UIType.Setting));
        exitBtn.onClick.AddListener(Application.Quit);
    }

    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Lobby);
    }

    private void OnClickStartButton()
    {
        StartCoroutine(UIManager.Instance.Fade(0, 1, 2, () => GameManager.ChangeScene(SceneType.Main)));
    }
}