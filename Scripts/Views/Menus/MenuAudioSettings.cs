using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioSettings : MonoBehaviour
{
    Button _closeButton;
    Button _musicMuteButton;
    Button _sfxMuteButton;
    Slider _sfxVolumenSlider;
    Slider _musicVolumenSlider;

    [SerializeField] Sprite _mutedSprite;
    [SerializeField] Sprite _unmutedSprite;
    private void Start()
    {
        FormController.Instance.MenuAudioSettings = this;

        InitializeComponent();
    }
    public void InitializeComponent()
    {
        _closeButton = transform.Find("CloseButton").GetComponent<Button>();
        _musicMuteButton = transform.Find("Background/Background2/MusicMuteButton").GetComponent<Button>();
        _sfxMuteButton = transform.Find("Background/Background2/SfxMuteButton").GetComponent<Button>();
        _musicVolumenSlider = transform.Find("Background/Background2/MusicVolumenSlider").GetComponent<Slider>();
        _sfxVolumenSlider = transform.Find("Background/Background2/SfxVolumenSlider").GetComponent<Slider>();

        _sfxVolumenSlider.onValueChanged.AddListener(delegate { SoundController.Instance.ChangeSfxVolumen(_sfxVolumenSlider.value); });
        _musicVolumenSlider.onValueChanged.AddListener(delegate { SoundController.Instance.ChangeMusicVolumen(_musicVolumenSlider.value); });

        ChangeMusicMuteButtonSprite(true);
        ChangeSfxMuteButtonSprite(true);
        UpdateSfxSlider();
        UpdateMusicSlider();

        _closeButton.onClick.AddListener(() => FormController.Instance.CloseMenu(0.5f));

        _musicMuteButton.onClick.AddListener(() => ChangeMusicMuteButtonSprite());
        _sfxMuteButton.onClick.AddListener(() => ChangeSfxMuteButtonSprite());
    }

    public void ChangeSfxMuteButtonSprite(bool _onlyVisualState = false)
    {
        if (!_onlyVisualState)
        {
            SoundController.Instance.MuteSFX();
        }

        if (SoundController.Instance.SfxMuted)
        {
            _sfxMuteButton.GetComponent<Image>().sprite = _mutedSprite;
        }

        else
        {
            _sfxMuteButton.GetComponent<Image>().sprite = _unmutedSprite;
        }

        UpdateSfxSlider();
    }

    public void ChangeMusicMuteButtonSprite(bool _onlyVisualState = false)
    {
        if (!_onlyVisualState)
        {
            SoundController.Instance.MuteMusic();
        }

        if (SoundController.Instance.MusicMuted)
        {
            _musicMuteButton.GetComponent<Image>().sprite = _mutedSprite;
        }

        else
        {
            _musicMuteButton.GetComponent<Image>().sprite = _unmutedSprite;
        }

        UpdateMusicSlider();
    }

    public void UpdateMusicSlider()
    {
        if (SoundController.Instance.MusicMuted)
        {
            _musicVolumenSlider.value = 0;
            _musicVolumenSlider.interactable = false;
        }
        else
        {
            _musicVolumenSlider.value = SoundController.Instance.MusicVolumenValue;
            _musicVolumenSlider.interactable = true;
        }
    }

    public void UpdateSfxSlider()
    {
        if (SoundController.Instance.SfxMuted)
        {
            _sfxVolumenSlider.value = 0;
            _sfxVolumenSlider.interactable = false;
        }
        else
        {
            _sfxVolumenSlider.value = SoundController.Instance.SfxVolumenValue;
            _sfxVolumenSlider.interactable = true;
        }
    }

}
