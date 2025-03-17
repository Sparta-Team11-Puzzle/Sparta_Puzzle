using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    [SerializeField] private Vector3 openRotation;

    public void Start()
    {
        openRotation += doorObject.eulerAngles;
    }

    public void EventTrigger()
    {
        doorObject.DORotate(openRotation, 2f);
    }
}
