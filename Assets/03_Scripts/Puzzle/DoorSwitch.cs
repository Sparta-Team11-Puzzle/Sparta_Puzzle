using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    [Header("Switch")]
    [SerializeField] private Transform leverTransform;

    [Header("Switch Target Drag and Drop")]
    [SerializeField] private GameObject[] targetObject;
    private IEventTrigger[] targetEvent;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private Vector3 leverRotation;

    void Start()
    {
        leverRotation = new Vector3(0, 0, 33);  // 레버 당겼을때 각도

        if (targetObject == null)
            return;

        targetEvent = new IEventTrigger[targetObject.Length];
        for(int i = 0; i < targetObject.Length; i++)
        {
            targetObject[i].TryGetComponent(out targetEvent[i]);
        }

        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void Interact()
    {
        audioSource.Play();
        leverTransform.DORotate(leverRotation, 2f);
        leverRotation *= -1;

        for (int i = 0; i < targetObject.Length; i++)
        {
            targetEvent[i]?.EventTrigger();
        }
    }
}
