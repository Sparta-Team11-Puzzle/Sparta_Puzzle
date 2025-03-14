using DataDeclaration;
using TMPro;
using UnityEngine;

/// <summary>
/// 인게임 UI 클래스
/// </summary>
public class InGameUI : BaseUI
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
        uiType = UIType.InGame;
    }
    
    public override void ActiveUI(UIType type)
    {
        gameObject.SetActive(type == UIType.InGame);
    }

    /// <summary>
    /// 플레이 타임 계산
    /// </summary>
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
