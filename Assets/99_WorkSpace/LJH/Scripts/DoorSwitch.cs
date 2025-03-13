using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject targetObject;
    private IEventTrigger targetEvent;

    void Start()
    {
        if (targetObject == null)
            return;

        targetEvent = targetObject.GetComponent<IEventTrigger>();
    }

    public void Interact()
    {
        targetEvent?.EventTrigger();
    }

    public void Active()
    {
        Debug.Log("DoorSwitch activated!");
        Interact(); // 필요하면 기존 Interact() 호출
    }
}
