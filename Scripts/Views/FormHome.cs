using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class FormHome : MonoBehaviour
{
    string _minigamesGeneralThumbnailsPath = "Sprites/Minigames/Thumbnails/";
    string _minigamesButtonComponentPath = "Prefabs/Views/Components/GameButton";
    ScrollRect _minigamesScrollView;
    TextMeshProUGUI _userNameText;
    TextMeshProUGUI _userAgeText;
    Button _audioSettingsButton;
    ButtonSoundsManager _buttonSoundsManager;

    Dictionary<int, Enumerators.LanguageCode> _languagesDropdownValues = new Dictionary<int, Enumerators.LanguageCode>();
    TMP_Dropdown _languagesDropdown;

    [SerializeField] Sprite _background;
    public Sprite Background { get => _background; set => _background = value; }

    public TextMeshProUGUI UserNameText { get => _userNameText; set => _userNameText = value; }
    public TextMeshProUGUI UserAgeText { get => _userAgeText; set => _userAgeText = value; }


    private void Awake()
    {
        FormController.Instance.FormHome = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _buttonSoundsManager = GetComponent<ButtonSoundsManager>();
        _minigamesScrollView = transform.Find("MinigamesScrollView").GetComponent<ScrollRect>();
        UserNameText = transform.Find("UserInfo/UserName").GetComponent<TextMeshProUGUI>();
        UserAgeText = transform.Find("UserInfo/UserAge").GetComponent<TextMeshProUGUI>();
        _languagesDropdown = transform.Find("LanguageDropdown").GetComponent<TMP_Dropdown>();
        _audioSettingsButton = transform.Find("AudioSettingsButton").GetComponent<Button>();


        UpdateUserInformation();
        InstantiateMinigameButtons();
        ChargeLanguageDropdown();

        _languagesDropdown.onValueChanged.AddListener(delegate { FormController.Instance.ChangeLanguage(_languagesDropdownValues[_languagesDropdown.value].ToString()); });
        _audioSettingsButton.onClick.AddListener(() => FormController.Instance.InstantiateMenu(MenusEnum.AudioSettings));

        FormController.Instance.ChangeFormLanguage();
    }

    public void UpdateUserInformation()
    {
        UserNameText.text = DataManager.Instance.CurrentUser.UserName;
        UserAgeText.text = DataManager.Instance.CurrentUser.UserAge.ToString();
    }

    public void InstantiateMinigameButtons()
    {
        foreach (var item in DataManager.Instance.MinigamesDictionary)
        {
            if (item.Value.RecommendedAge <= DataManager.Instance.CurrentUser.UserAge)
            {
                GameObject _currentMinigameButton = (GameObject)Instantiate(Resources.Load(_minigamesButtonComponentPath), _minigamesScrollView.content);
                _currentMinigameButton.GetComponent<Button>().onClick.AddListener(() => MinigamesController.Instance.PlayMinigame(item.Value.GameId));
                _currentMinigameButton.transform.Find("GameThumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>(_minigamesGeneralThumbnailsPath + item.Value.ThumbnailPath);
                _currentMinigameButton.transform.Find("GameName").GetComponent<TextMeshProUGUI>().text = item.Value.MinigameName;
                _buttonSoundsManager.AddButtonSound(_currentMinigameButton.GetComponent<Button>(), 0);
            }
        }
    }

    public void ChargeLanguageDropdown()
    {
        _languagesDropdown.ClearOptions();

        List<string> _currentLanguageOptions = new List<string>();
        int _currentIndex = 0;
        int _indexCurrentLanguage = 0;

        foreach (var item in FormController.Instance.LanguagesValueDictionary)
        {
            if (DataManager.Instance.CurrentLanguage != "")
            {
                if (item.Key.ToString() == DataManager.Instance.CurrentLanguage)
                {
                    _indexCurrentLanguage = _currentIndex;
                }
            }

            _currentLanguageOptions.Add(item.Key.ToString());
            _languagesDropdownValues.Add(_currentIndex, item.Key);
            _currentIndex++;
        }

        _languagesDropdown.AddOptions(_currentLanguageOptions);
        _languagesDropdown.value = _indexCurrentLanguage;

    }

}
