using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorHJM : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    public void EventTrigger()
    {
        // ������Ʈ ��Ȱ��ȭ ( �׽�Ʈ )
        doorObject.gameObject.SetActive(false); 
    }
}
