using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorHJM : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    public void EventTrigger()
    {
        // 오브젝트 비활성화 ( 테스트 )
        doorObject.gameObject.SetActive(false); 
    }
}
