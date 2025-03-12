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
        startBtn.onClick.AddListener(() => GameManager.ChangeScene(SceneType.Main));
        settingBtn.onClick.AddListener(() => uiManager.ChangeUIState(UIType.Setting));
        exitBtn.onClick.AddListener(Application.Quit);
    }

    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Lobby);
    }
}