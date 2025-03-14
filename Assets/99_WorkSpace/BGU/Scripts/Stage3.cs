using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : BaseRoom
{
    // 방 초기화 할때 플레이어한테 PushAction 붙이기
    public override void InitRoom(DungeonSystem system)
    {
        base.InitRoom(system);

        Player player = CharacterManager.Instance.Player;     // Charactormagaer 가져오도록 설정

        if (player == null) return;

        if (player.GetComponent<PushAction>() == null)
        {
            player.gameObject.AddComponent<PushAction>();
        }
    }
}
