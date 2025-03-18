using DataDeclaration;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DungeonDoor : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    [SerializeField] private Vector3 openRotation;
    private bool openState;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;

    public void Start()
    {
        openRotation += doorObject.eulerAngles;
        openState = false;
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void EventTrigger()
    {
        if (openState)
            return;

        openState = true;
        audioSource.Play();
        doorObject.DORotate(openRotation, 2f);
    }
}
