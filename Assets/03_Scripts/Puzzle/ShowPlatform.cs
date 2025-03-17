using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlatform : MonoBehaviour
{
    private Action showPlatform;
    private Action hidePlatform;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    public void InitObject(Action showPlatform, Action hidePlatform)
    {
        this.showPlatform = showPlatform;
        this.hidePlatform = hidePlatform;

        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 트리거에 진입 시 발동
            if(showPlatform != null)
            {
                audioSource.Play();
                StartCoroutine(OnPlatform());
            }
        }
    }

    IEnumerator OnPlatform()
    {
        showPlatform?.Invoke(); // 플랫폼 보이기
        yield return new WaitForSeconds(5f);
        hidePlatform?.Invoke(); // 플랫폼 가리기
    }
}
