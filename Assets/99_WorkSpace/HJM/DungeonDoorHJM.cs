using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorHJM : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    [SerializeField] private Vector3 openRotation;

    public void Start()
    {
        openRotation += doorObject.eulerAngles;
    }

    public void EventTrigger()
    {
        // ������Ʈ ��Ȱ��ȭ ( �׽�Ʈ )
        doorObject.DORotate(openRotation, 2f);
        //doorObject.gameObject.SetActive(false); 
    }
}
