using System.Collections;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    private AudioSource audioSource;

    [Header ("사운드 클립")]
    [SerializeField] private AudioClip[] walkSounds; // 걷기 사운드 클립 배열
    [SerializeField] private AudioClip jumpSound; //점프 사운드 클립
    [SerializeField] private AudioClip dieSound; //사망 사운드 클립

    [Header("사운드 설정")]
    [SerializeField] private float walkSoundDelay; //걷기 사운드 재생 간격
    [SerializeField] private float jumpSoundPlayTime; //점프 사운드 재생 시간
    [SerializeField] private float dieSoundPlayTime; //사망 사운드 재생 시간

    private Coroutine walkSoundCorutine;

    private void Start()
    {
        //컴포넌트를 변수에 할당
        audioSource = GetComponent<AudioSource>();
        //음량을 AudioManager에서 한번에 관리하도록 목록에 추가
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    public void PlayWalkSound()
    {
        //사운드 재생 코루틴이 비어있을 때
        if (walkSoundCorutine == null)
        {
            //걷기 사운드 배열에서 무작위 소리를 재생
            int walkSoundIndex = Random.Range(0, walkSounds.Length);
            audioSource.PlayOneShot(walkSounds[walkSoundIndex]);
            //코루틴을 시작
            walkSoundCorutine = StartCoroutine(PlayRandomWalkSound());
        }
    }

    IEnumerator PlayRandomWalkSound()
    {
        //지정된 시간이 지날 때까지 대기
        yield return new WaitForSeconds(walkSoundDelay);
        //무작위 걷기 소리를 재생
        int walkSoundIndex = Random.Range(0, walkSounds.Length);
        audioSource.PlayOneShot(walkSounds[walkSoundIndex]);
        walkSoundCorutine = null;
    }

    public void StopWalkSound()
    {
        //걷기가 종료되었을 때 진행 중인 코루틴을 정지
        StopCoroutine(PlayRandomWalkSound());
    }

    public void PlayJumpSound()
    {
        //점프 사운드를 재생
        audioSource.PlayOneShot(jumpSound);
        StartCoroutine(StopJumpSound());
    }

    IEnumerator StopJumpSound()
    {
        //일정 시간이 지난 후 사운드 재생을 정지
        yield return new WaitForSeconds(jumpSoundPlayTime);
        audioSource.Stop();
    }

    public void PlayDieSound()
    {
        //사망 사운드를 재생
        audioSource.PlayOneShot(dieSound);
        StartCoroutine(StopDieSound());
    }

    IEnumerator StopDieSound()
    {
        //일정 시간이 지난 후 사운드 재생을 정지
        yield return new WaitForSeconds(dieSoundPlayTime);
        audioSource.Stop();
    }
}
