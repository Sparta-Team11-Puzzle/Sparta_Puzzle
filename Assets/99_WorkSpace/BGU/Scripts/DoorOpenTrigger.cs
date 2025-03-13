using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public string targetTag;
    [SerializeField] private GameObject openObject;
    private IEventTrigger targetEvent;

    void Start()
    {
        if (openObject == null)
            return;

        targetEvent = openObject.GetComponent<IEventTrigger>();
    }

    public void Interact()
    {
        targetEvent?.EventTrigger();
    }
}
