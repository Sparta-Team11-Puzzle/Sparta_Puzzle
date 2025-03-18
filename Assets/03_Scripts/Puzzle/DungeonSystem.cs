using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Stage1,
    Stage2,
    Stage3
}
public class DungeonSystem : MonoBehaviour
{
    private string saveKey = "DungeonKey";
    [SerializeField] private BaseRoom[] stages;
    [SerializeField] private StageType currentStage;
    private Transform playerTransform;
    private InGameUI inGameUI;

    private void Start()
    {
        // 캐싱
        playerTransform = CharacterManager.Instance.Player.transform;
        inGameUI = UIManager.Instance.InGameUI;

        // 데이터 불러오기
        LoadData();
        UpdateCurrentStage();

        // 등록해둔 스테이지 초기화
        for (int i = 0; i < stages.Length; i++)
            stages[i].InitRoom(this);

        // 저장 스테이지 스폰위치로 이동
        StartCoroutine(MoveToSpawnPoint());
    }

    /// <summary>
    /// 플레이어의 시작위치 설정 
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToSpawnPoint()
    {
        Rigidbody tRigidBody = playerTransform.GetComponent<Rigidbody>();

        // 물리엔진을 무시하고 position을 이동하기위함
        tRigidBody.isKinematic = true;

        // 저장된 스테이지의 스폰위치로 플레이어 이동
        playerTransform.position = stages[(int)currentStage].SpawnPoint.position;
         
        yield return null;
        tRigidBody.isKinematic = false;
    }

    /// <summary>
    /// 스테이지 위치 불러오기
    /// </summary>
    private void LoadData()
    {
        currentStage = (StageType)PlayerPrefs.GetInt(saveKey, 0);
    }

    /// <summary>
    /// 스테이지 위치 저장
    /// </summary>
    /// <param name="stage">저장할 스테이지</param>
    public void SaveData(StageType stage)
    {
        currentStage = stage;
        PlayerPrefs.SetInt(saveKey, (int)stage);

        UpdateCurrentStage();
    }

    /// <summary>
    /// 현재 스테이지 표시
    /// </summary>
    private void UpdateCurrentStage()
    {
        inGameUI.CurStage.text = $"현재 스테이지 : {(int)currentStage + 1}";
    }
}