using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    protected bool isClear;             // �� Ŭ���� ����
    protected bool roomInPlayer;        // �÷��̾� ���� ����

    protected GameObject roomObjects;   // ���� ������Ʈ
    protected Transform player;         // �÷��̾� Transform

    protected DungeonManager dungeonManager;

    public abstract void InitRoom(DungeonManager manager);
    public abstract void UpdateRoom();
}
