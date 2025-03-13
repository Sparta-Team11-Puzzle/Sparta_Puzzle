using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private List<AudioSource> sfxList;

    private float masterVol;
    private float bgmVol;
    private float sfxVol;

    private AudioSource bgm;
    [SerializeField] private AudioClip bgmClip;
    
    protected override void Awake()
    {
        base.Awake();
        
        sfxList =  new List<AudioSource>();
        
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
        sfxList.RemoveAll(x => x == null);
    }

    public void ChangeMasterVol(float figure)
    {
        masterVol = figure;
        bgm.volume = bgmVol * masterVol;
        foreach (var sfx in sfxList)
        {
            sfx.volume = sfxVol * masterVol;
        }
    }

    public void ChangeBGMVol(float figure)
    {
        bgmVol = figure;
        bgm.volume = bgmVol * masterVol;
    }
    
    public void ChangeSFXVol(float figure)
    {
        sfxVol = figure;
        foreach (var sfx in sfxList)
        {
            sfx.volume = sfxVol * masterVol;
        }
    }

    public void AddSFXAudioSource(AudioSource audioSource)
    {
        if (sfxList.Contains(audioSource)) return;
        sfxList.Add(audioSource);
        audioSource.volume = sfxVol * masterVol;
    }

    public void RemoveSFXAudioSource(AudioSource audioSource)
    {
        if (sfxList.Contains(audioSource))
        {
            sfxList.Remove(audioSource);
        }
    }
}
