using DataDeclaration;
using TMPro;
using UnityEngine;

public class MainUI : BaseUI
{
    [SerializeField] private GameObject interactGuide;
    [SerializeField] private GameObject keyGuide;
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private TextMeshProUGUI curStage;

    private float playTimeSec;
    private int playTimeMin;

    public GameObject InteractGuide => interactGuide;
    private TextMeshProUGUI CurStage => curStage;

    private void Update()
    {
        SetPlayTime();
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
        uiType = UIType.Main;
    }
    
    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.Main);
    }

    private void SetPlayTime()
    {
        playTimeSec += Time.unscaledDeltaTime;
        if (playTimeSec >= 60f)
        {
            playTimeMin += 1;
            playTimeSec = 0;
        }
        
        playTime.text = $"진행 시간 {playTimeMin:D2}:{Mathf.FloorToInt(playTimeSec):D2}";
    }
}
