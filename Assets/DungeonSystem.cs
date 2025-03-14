using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSystem : MonoBehaviour
{
    [SerializeField] private BaseRoom[] stages;
    
    private void Start()
    {
        // 등록해둔 스테이지 초기화
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].InitRoom(this);
        }
    }
}