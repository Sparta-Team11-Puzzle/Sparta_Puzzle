using System;
using System.Collections;
using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIManager : Singleton<UIManager>
{
    private bool isCursorOn;
    private CanvasGroup fader;
    [SerializeField] private AudioClip buttonSound;

    private AudioSource audioSource;

    public List<BaseUI> UIList { get; private set; }
    public LobbyUI LobbyUI { get; private set; }
    public SettingUI SettingUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        if (buttonSound == null)
        {
            buttonSound = Resources.Load<AudioClip>("Audio/button_press_1");
        }

        audioSource = GetComponent<AudioSource>();

        UIList = new List<BaseUI>();
        LobbyUI = InitUI<LobbyUI>();
        SettingUI = InitUI<SettingUI>();
    }

    private void Start()
    {
        AudioManager.Instance.AddSFXAudioSource(audioSource);

        InitFader();
        ChangeUIState(UIType.Lobby);
    }

    /// <summary>
    /// 마우스 커서 On/Off 기능
    /// </summary>
    /// <param name="isActive">True: 마우스활성화 False: 마우스 비활성화</param>
    public static void ToggleCursor(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void ChangeUIState(UIType type)
    {
        foreach (var ui in UIList)
        {
            ui.ActiveUI(type);
        }
    }

    public void Fade(float startAlpha, float endAlpha, float fadeTime, Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(startAlpha,  endAlpha, fadeTime, onComplete));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float fadeTime, Action onComplete)
    {
        var elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fader.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeTime);
            yield return null;
        }

        fader.alpha = endAlpha;
        onComplete?.Invoke();
    }

    private T InitUI<T>() where T : BaseUI
    {
        var ui = GetComponentInChildren<T>(true);
        if (ui == null)
        {
            var go = new GameObject(nameof(T));
            go.transform.SetParent(transform);
            ui = go.AddComponent<T>();
        }

        ui.Init(this);
        return ui;
    }

    private void InitFader()
    {
        fader = FindObjectOfType<CanvasGroup>();
        if (fader != null) return;
        var go = Resources.Load<GameObject>("Prefab/UI/Fader");
        fader = Instantiate(go).GetComponent<CanvasGroup>();
    }
}