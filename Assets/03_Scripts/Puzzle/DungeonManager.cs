using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    Stage1,
    Stage2,
    Stage3,
}
public class DungeonManager : Singleton<DungeonManager>
{
    [SerializeField] private BaseRoom[] baseRooms;
    [SerializeField] private BaseRoom currentRoom;
    private Stage currentStageType;

    private void Start()
    {
        InitDungeon();
    }

    void Update()
    {
        if (currentRoom == null)
            return;

        // 현재 스테이지의 로직 실행
        currentRoom.UpdateRoom();
    }

    public void InitDungeon()
    {
        // 등록해둔 스테이지 초기화
        for (int i = 0; i < baseRooms.Length; i++)
        {
            baseRooms[i].InitRoom(this);
        }

        // 현재 스테이지를 1스테이지로 변경
        ChangeRoom(Stage.Stage1);
    }

    public void ChangeRoom(Stage stage)
    {
        currentStageType = stage;
        currentRoom = baseRooms[(int)stage];
    }

    public string GetCurrentStageName()
    {
        return currentStageType.ToString();
    }
}
