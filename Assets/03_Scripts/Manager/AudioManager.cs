using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private List<AudioSource> sfxList; // SFX 볼륨 관리용 리스트

    private float masterVol;
    private float bgmVol;
    private float sfxVol;

    private AudioSource bgm;
    [SerializeField] private AudioClip bgmClip;

    protected override void Awake()
    {
        base.Awake();

        sfxList = new List<AudioSource>();

        masterVol = 0.8f;
        bgmVol = 0.8f;
        sfxVol = 0.8f;

        bgm = GetComponent<AudioSource>();
        bgm.clip = bgmClip;
        bgm.volume = bgmVol * masterVol;

        PlayerPrefs.SetFloat(Constant.MASTER_VOL, masterVol);
        PlayerPrefs.SetFloat(Constant.BGM_VOL, bgmVol);
        PlayerPrefs.SetFloat(Constant.SFX_VOL, sfxVol);
    }

    private void Start()
    {
        bgm.Play();
    }

    private void OnDestroy()
    {
        // 리스트 메모리 해제
        sfxList.RemoveAll(x => x == null);
    }

    /// <summary>
    /// 마스터 볼륨 조절 기능
    /// </summary>
    /// <param name="figure">0~1 사이 값</param>
    public void ChangeMasterVol(float figure)
    {
        masterVol = figure;
        bgm.volume = bgmVol * masterVol;
        foreach (var sfx in sfxList)
        {
            sfx.volume = sfxVol * masterVol;
        }
    }

    /// <summary>
    /// 배경음악 불륨 조절 기능
    /// </summary>
    /// <param name="figure">0~1 사이 값</param>
    public void ChangeBGMVol(float figure)
    {
        bgmVol = figure;
        bgm.volume = bgmVol * masterVol;
    }

    /// <summary>
    /// SFX 볼륨 조절 기능
    /// </summary>
    /// <param name="figure">0~1 사이 값</param>
    public void ChangeSFXVol(float figure)
    {
        sfxVol = figure;
        foreach (var sfx in sfxList)
        {
            sfx.volume = sfxVol * masterVol;
        }
    }

    /// <summary>
    /// 씬에 올라간 SFX AudioSource 리스트에 저장
    /// </summary>
    /// <param name="audioSource">SFX용 AudioSource</param>
    public void AddSFXAudioSource(AudioSource audioSource)
    {
        if (sfxList.Contains(audioSource)) return;
        sfxList.Add(audioSource);
        audioSource.volume = sfxVol * masterVol;
    }

    /// <summary>
    /// SFX AudioSource 리스트에서 해제
    /// </summary>
    /// <param name="audioSource">SFX용 AudioSource</param>
    public void RemoveSFXAudioSource(AudioSource audioSource)
    {
        if (sfxList.Contains(audioSource))
        {
            sfxList.Remove(audioSource);
        }
    }
}