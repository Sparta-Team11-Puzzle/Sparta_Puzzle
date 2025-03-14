using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : BaseRoom
{
    // 방 초기화 할때 플레이어한테 PushAction 붙이기
    public override void InitRoom(DungeonManager manager)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        if(player.GetComponent<Player>() == null)
        {
            player.AddComponent<PushAction>();
        }
    }
}
