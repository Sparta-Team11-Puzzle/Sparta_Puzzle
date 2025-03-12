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
    private BaseRoom[] baseRooms;
    private BaseRoom currentRoom;
    
    void Start()
    {
        for (int i = 0; i < baseRooms.Length; i++)
        {
            baseRooms[i].InitRoom(this);
        }
    }

    void Update()
    {
        if (currentRoom == null)
            return;

        currentRoom.UpdateRoom();
    }

    // #TODO
    // �� ���� �Լ� �߰��ϱ�
    // 
}
