using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    private AudioSource audioSource;

    [Header ("사운드 클립")]
    [SerializeField] private AudioClip[] walkSounds;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dieSound;

    [Header("사운드 설정")]
    [SerializeField] private float walkSoundDelay;
    [SerializeField] private float jumpSoundPlayTime;
    [SerializeField] private float dieSoundPlayTime;

    private Coroutine walkSoundCorutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void PlayWalkSound()
    {
        if (walkSoundCorutine == null)
        {
            int walkSoundIndex = Random.Range(0, walkSounds.Length);
            audioSource.PlayOneShot(walkSounds[walkSoundIndex]);

            walkSoundCorutine = StartCoroutine(PlayRandomWalkSound());
        }
    }

    IEnumerator PlayRandomWalkSound()
    {
        yield return new WaitForSeconds(walkSoundDelay);

        int walkSoundIndex = Random.Range(0, walkSounds.Length);
        audioSource.PlayOneShot(walkSounds[walkSoundIndex]);
        walkSoundCorutine = null;
    }

    public void StopWalkSound()
    {
        StopCoroutine(PlayRandomWalkSound());
    }

    public void PlayJumpSound()
    {
        audioSource.time = jumpSoundPlayTime;
        audioSource.PlayOneShot(jumpSound);
        StartCoroutine(StopJumpSound());
    }

    IEnumerator StopJumpSound()
    {
        yield return new WaitForSeconds(jumpSoundPlayTime);
        audioSource.Stop();
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieSound);
        StartCoroutine(StopDieSound());
    }

    IEnumerator StopDieSound()
    {
        yield return new WaitForSeconds(dieSoundPlayTime);
        audioSource.Stop();
    }
}
