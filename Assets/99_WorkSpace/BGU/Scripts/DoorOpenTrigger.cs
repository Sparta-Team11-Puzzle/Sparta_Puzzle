using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject openObject;
    public string targetTag;
    private IEventTrigger targetEvent;
    private bool isTriggered = false;           // 트리거 상태 확인

    void Start()
    {
        if (openObject == null)
            return;

        targetEvent = openObject.GetComponent<IEventTrigger>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;    // 이미 트리거 시 무시

        if (other.CompareTag(targetTag))
        {
            isTriggered = true;     // 트리거 표시
            targetEvent?.EventTrigger();
        }
    }

    public void Interact()
    {
        if (isTriggered) return;

        isTriggered = true;     // 트리거 표시
        targetEvent?.EventTrigger();
    }
}
