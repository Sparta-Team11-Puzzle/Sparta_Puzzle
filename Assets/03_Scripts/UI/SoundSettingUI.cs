using DataDeclaration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 사운드 환경 설정 클래스
/// </summary>
public class SoundSettingUI : MonoBehaviour,  ISettingUI
{
    private AudioManager audioManager;
    
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private TextMeshProUGUI masterVolText;
    [Space]
    [SerializeField] private Slider bgmVolSlider;
    [SerializeField] private TextMeshProUGUI bgmVolText;
    [Space]
    [SerializeField] private Slider sfxVolSlider;
    [SerializeField] private TextMeshProUGUI sfxVolText;

    private void OnEnable()
    {
        if (audioManager != null)
        {
            InitUI();
        }
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;

        InitUI();
        
        masterVolSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmVolSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxVolSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        audioManager.ChangeMasterVol(value);
        masterVolText.text =  (value * 100f).ToString("N0");
    }

    private void OnBGMVolumeChanged(float value)
    {
        audioManager.ChangeBGMVol(value);
        bgmVolText.text = (value * 100f).ToString("N0");
    }

    private void OnSFXVolumeChanged(float value)
    {
        audioManager.ChangeSFXVol(value);
        sfxVolText.text = (value * 100f).ToString("N0");
    }
    
    private void InitUI()
    {
        audioManager.LoadUserAudioSetting();
        
        masterVolSlider.value = audioManager.masterVol;
        masterVolText.text =  (masterVolSlider.value * 100f).ToString("N0");
        
        bgmVolSlider.value = audioManager.bgmVol;
        bgmVolText.text =  (bgmVolSlider.value * 100f).ToString("N0");
        
        sfxVolSlider.value = audioManager.sfxVol;
        sfxVolText.text =  (sfxVolSlider.value * 100f).ToString("N0");
    }

    void ISettingUI.OnClickCancelButton()
    {
        OnMasterVolumeChanged(PlayerPrefs.GetFloat(Constant.MASTER_VOL));
        masterVolSlider.value = PlayerPrefs.GetFloat(Constant.MASTER_VOL);
        OnBGMVolumeChanged(PlayerPrefs.GetFloat(Constant.BGM_VOL));
        bgmVolSlider.value = PlayerPrefs.GetFloat(Constant.BGM_VOL);
        OnSFXVolumeChanged(PlayerPrefs.GetFloat(Constant.SFX_VOL));
        sfxVolSlider.value = PlayerPrefs.GetFloat(Constant.SFX_VOL);
        
        UIManager.Instance.PlayButtonSound();
    }

    void ISettingUI.OnClickApplyButton()
    {
        audioManager.SaveUserAudioSetting();
        UIManager.Instance.PlayButtonSound();
    }
}
