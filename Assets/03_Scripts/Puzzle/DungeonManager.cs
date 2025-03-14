using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    // ���� �ʿ��ϸ� �����ϱ�
    Stage1,
    Stage2,
    Stage3,
}
public class DungeonManager : MonoBehaviour
{
    [SerializeField] private BaseRoom[] baseRooms;
    [SerializeField] private BaseRoom currentRoom;
    private Stage currentStageType;
    
    void Start()
    {
        for (int i = 0; i < baseRooms.Length; i++)
        {
            baseRooms[i].InitRoom(this);
        }

        ChangeRoom(Stage.Stage1);
    }

    void Update()
    {
        if (currentRoom == null)
            return;

        currentRoom.UpdateRoom();
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
