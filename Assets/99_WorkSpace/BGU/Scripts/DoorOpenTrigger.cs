using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject openObject;
    public string targetTag;
    private IEventTrigger targetEvent;
    private bool isTriggered = false;           // Ʈ���� ���� Ȯ��

    void Start()
    {
        if (openObject == null)
            return;

        targetEvent = openObject.GetComponent<IEventTrigger>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;    // �̹� Ʈ���� �� ����

        if (other.CompareTag(targetTag))
        {
            isTriggered = true;     // Ʈ���� ǥ��
            targetEvent?.EventTrigger();
        }
    }

    public void Interact()
    {
        if (isTriggered) return;

        isTriggered = true;     // Ʈ���� ǥ��
        targetEvent?.EventTrigger();
    }
}
