using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour
{
    protected bool isClear;             // 방 클리어 여부
    protected bool roomInPlayer;        // 플레이어 존재 여부

    protected Transform player;                         // 플레이어 Transform
    [SerializeField] protected Transform spawnPoint;    // 플레이어 생성 위치

    protected DungeonSystem dungeon;

    // Get / Set
    public Transform SpawnPoint { get { return spawnPoint; } }

    public virtual void InitRoom(DungeonSystem dungeon)
    {
        this.dungeon = dungeon;

        player = CharacterManager.Instance.Player.transform;
    }
}
