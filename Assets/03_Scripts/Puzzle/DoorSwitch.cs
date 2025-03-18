using DataDeclaration;
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
        // 레버 상호작용시 레버 움직임 각도
        leverRotation = new Vector3(0, 0, 33);

        if (targetObject == null)
            return;

        // 레버 상호작용시 동작할 오브젝트 인터페이스
        targetEvent = new IEventTrigger[targetObject.Length];
        for(int i = 0; i < targetObject.Length; i++)
        {
            targetObject[i].TryGetComponent(out targetEvent[i]);
        }

        // 오디오
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void Interact()
    {
        audioSource.Play();
        
        // 레버 움직임
        leverTransform.DORotate(leverRotation, 2f);
        leverRotation *= -1;

        // 인터페이스 실행
        for (int i = 0; i < targetObject.Length; i++)
        {
            targetEvent[i]?.EventTrigger();
        }
    }
}
