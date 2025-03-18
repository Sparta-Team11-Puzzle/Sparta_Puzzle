using DataDeclaration;
using DG.Tweening;
using UnityEngine;

public class DungeonDoor : MonoBehaviour, IEventTrigger
{
    [SerializeField] private Transform doorObject;
    [SerializeField] private Vector3 openRotation;
    private bool openState;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;

    public void Start()
    {
        // Door 움직임 각도
        openRotation += doorObject.eulerAngles;
        openState = false;

        // 오디오
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void EventTrigger()
    {
        // 이미 열린상태일때
        if (openState)
            return;

        audioSource.Play();

        // 문 동작
        openState = true;
        doorObject.DORotate(openRotation, 2f);
    }
}
