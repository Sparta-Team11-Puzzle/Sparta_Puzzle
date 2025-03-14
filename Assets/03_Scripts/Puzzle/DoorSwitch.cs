using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    [Header("Switch")]
    [SerializeField] private Transform leverTransform;

    [Header("Switch Target Drag and Drop")]
    [SerializeField] private GameObject targetObject;
    private IEventTrigger targetEvent;


    private Vector3 leverRotation;

    void Start()
    {
        leverRotation = new Vector3(0, 0, 33);  // 레버 당겼을때 각도

        if (targetObject == null)
            return;

        targetEvent = targetObject.GetComponent<IEventTrigger>();
    }

    public void Interact()
    {
        Debug.Log(gameObject.name + " on");

        leverTransform.DORotate(leverRotation, 2f);
        leverRotation *= -1;

        targetEvent?.EventTrigger();


    }
}
