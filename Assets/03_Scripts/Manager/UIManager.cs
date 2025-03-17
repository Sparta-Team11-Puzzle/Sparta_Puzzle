using System;
using System.Collections;
using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI 관리 클래스(싱글톤)
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class UIManager : Singleton<UIManager>, IOnSceneLoaded
{
    private bool isCursorOn;
    private CanvasGroup fader; // Fade 연출 오브젝트
    [SerializeField] private AudioClip buttonSound; // 버튼 클릭 SFX

    private AudioSource audioSource; // SFX용 AudioSource

    public List<BaseUI> UIList { get; private set; }
    public LobbyUI LobbyUI { get; private set; }
    public SettingUI SettingUI { get; private set; }
    public PauseUI PauseUI { get; private set; }
    public InGameUI InGameUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (buttonSound == null)
        {
            buttonSound = Resources.Load<AudioClip>("Audio/button_press_1");
        }

        audioSource = GetComponent<AudioSource>();

        UIList = new List<BaseUI>();
    }

    private void Start()
    {
        AudioManager.Instance.AddSFXAudioSource(audioSource);
    }

    private void OnDestroy()
    {
        UIList.Clear();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 마우스 커서 On/Off 기능
    /// </summary>
    /// <param name="isActive">True: 마우스활성화 False: 마우스 비활성화</param>
    public static void ToggleCursor(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /// <summary>
    /// 버튼 클릭 SFX 출력
    /// </summary>
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    /// <summary>
    /// UI 변경 기능
    /// </summary>
    /// <param name="type">활성화 하고 싶은 UI 타입</param>
    public void ChangeUIState(UIType type)
    {
        foreach (var ui in UIList)
        {
            ui.ActiveUI(type);
        }
    }

    /// <summary>
    /// Fade 연출 기능
    /// </summary>
    /// <param name="startAlpha">시작 알파값</param>
    /// <param name="endAlpha">종료 알파값</param>
    /// <param name="fadeTime">연출 시간</param>
    /// <param name="onComplete">연출 종료 시 호출할 함수<para>기본값: null</para></param>
    public void Fade(float startAlpha, float endAlpha, float fadeTime, Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(startAlpha, endAlpha, fadeTime, onComplete));
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

    /// <summary>
    /// UIManager에 UI 저장 기능
    /// </summary>
    /// <typeparam name="T">BaseUI</typeparam>
    /// <returns>BaseUI를 상속받은 UI 클래스</returns>
    private T InitUI<T>() where T : BaseUI
    {
        var ui = GetComponentInChildren<T>(true);
        if (ui == null)
        {
            var prefab = Resources.Load<GameObject>("Prefab/UI/" + typeof(T).Name);
            var go = Instantiate(prefab);
            ui = go.GetComponent<T>();
            go.transform.SetParent(transform, false);
        }

        ui.Init(this);
        return ui;
    }

    /// <summary>
    /// Fader 초기화
    /// </summary>
    private void InitFader()
    {
        if (fader != null) return;
        fader = FindObjectOfType<CanvasGroup>();
        if (fader != null) return;
        var go = Resources.Load<GameObject>("Prefab/UI/Fader");
        fader = Instantiate(go).GetComponent<CanvasGroup>();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                ChangeUIState(UIType.None);
                UIList.Clear();
                if (LobbyUI == null)
                {
                    LobbyUI = InitUI<LobbyUI>();
                }
                else
                {
                    UIList.Add(LobbyUI);
                }
                if (SettingUI == null)
                {
                    SettingUI = InitUI<SettingUI>();
                }
                else
                {
                    UIList.Add(SettingUI);
                }
                InitFader();
                ChangeUIState(UIType.Lobby);
                break;
            case 1:
                ChangeUIState(UIType.None);
                UIList.Clear();
                if (PauseUI == null)
                {
                    PauseUI = InitUI<PauseUI>();
                }
                else
                {
                    UIList.Add(PauseUI);
                }
                if (InGameUI == null)
                {
                    InGameUI = InitUI<InGameUI>();
                }
                else
                {
                    UIList.Add(InGameUI);
                }
                if (SettingUI == null)
                {
                    SettingUI = InitUI<SettingUI>();
                }
                else
                {
                    UIList.Add(SettingUI);
                }
                InitFader();
                ChangeUIState(UIType.InGame);
                break;
        }
    }
}