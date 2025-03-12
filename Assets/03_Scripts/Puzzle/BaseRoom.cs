using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    protected bool isClear;             // �� Ŭ���� ����
    protected bool roomInPlayer;        // �÷��̾� ���� ����

    protected List<GameObject> roomObjects; // ���� ������Ʈ
    protected Transform player;         // �÷��̾� Transform

    protected DungeonManager dungeonManager;

    public virtual void InitRoom(DungeonManager manager)
    {
        dungeonManager = manager;
    }
    public abstract void UpdateRoom();
}
