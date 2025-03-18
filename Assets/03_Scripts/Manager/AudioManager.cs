using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 사운드 관리 클래스(싱글톤)
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>, IOnSceneLoaded
{
    private List<AudioSource> sfxList; // SFX 볼륨 관리용 리스트

    public float masterVol;
    public float bgmVol;
    public float sfxVol;

    private AudioSource bgm;
    private Dictionary<SceneType, AudioClip> bgmClipDict;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;

        sfxList = new List<AudioSource>();

        bgm = GetComponent<AudioSource>();
        bgm.loop = true;
        bgmClipDict = new Dictionary<SceneType, AudioClip>
        {
            { SceneType.Lobby, Resources.Load<AudioClip>("Audio/ha-waterheater") },
            { SceneType.Main, Resources.Load<AudioClip>("Audio/Ash_and_Dust") },
            { SceneType.Ending, Resources.Load<AudioClip>("Audio/The_Void") }
        };
        
        LoadUserAudioSetting();
        bgm.volume = bgmVol * masterVol;
    }

    private void Start()
    {
        bgm.clip = bgmClipDict[SceneType.Lobby];
        bgm.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
    
    public void LoadUserAudioSetting()
    {
        masterVol = PlayerPrefs.GetFloat(Constant.MASTER_VOL, 0.8f);
        bgmVol = PlayerPrefs.GetFloat(Constant.BGM_VOL, 0.8f);
        sfxVol = PlayerPrefs.GetFloat(Constant.SFX_VOL, 0.8f);
    }
    
    public void SaveUserAudioSetting()
    {
        PlayerPrefs.SetFloat(Constant.MASTER_VOL, masterVol);
        PlayerPrefs.SetFloat(Constant.BGM_VOL, bgmVol);
        PlayerPrefs.SetFloat(Constant.SFX_VOL, sfxVol);
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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                bgm.clip = bgmClipDict[SceneType.Lobby];
                bgm.Play();
                break;
            case 1:
                bgm.clip = bgmClipDict[SceneType.Main];
                bgm.Play();
                break;
            case 2:
                bgm.clip = bgmClipDict[SceneType.Ending];
                bgm.Play();
                break;
        }
    }
}