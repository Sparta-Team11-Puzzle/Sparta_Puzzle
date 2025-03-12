using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    protected bool isClear;             // 방 클리어 여부
    protected bool roomInPlayer;        // 플레이어 존재 여부

    protected List<GameObject> roomObjects; // 퍼즐 오브젝트
    protected Transform player;         // 플레이어 Transform

    protected DungeonManager dungeonManager;

    public virtual void InitRoom(DungeonManager manager)
    {
        dungeonManager = manager;
    }
    public abstract void UpdateRoom();
}
