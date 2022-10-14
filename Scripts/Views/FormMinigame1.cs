using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class FormMinigame1 : MonoBehaviour
{
    SpeechRecognitionManager _speechRecognitionManager;

    [SerializeField] Button _recordButton;
    [SerializeField] Button _stopButton;
    Image _recognitionStateImage;
    TextMeshProUGUI _questionText;
    TextMeshProUGUI _resultText;
    TextMeshProUGUI _answerText;
    Dropdown _languageDropdown;
    Button _audioSettingsButton;

    private void Awake()
    {
        FormController.Instance.FormMinigame1 = this;

        InitializeComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowQuestion();
        }
    }
    public void InitializeComponent()
    {
        _speechRecognitionManager = GetComponent<SpeechRecognitionManager>();

        _audioSettingsButton = transform.Find("AudioSettingsButton").GetComponent<Button>();
        _recordButton = transform.Find("RecordButton").GetComponent<Button>();
        _stopButton = transform.Find("StopButton").GetComponent<Button>();
        _questionText = transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        _answerText = transform.Find("AnswerText").GetComponent<TextMeshProUGUI>();
        _resultText = transform.Find("ResultText").GetComponent<TextMeshProUGUI>();
        _recognitionStateImage = transform.Find("RecognitionState").GetComponent<Image>();
        //_languageDropdown = transform.Find("LanguageDropdown").GetComponent<Dropdown>();

        _audioSettingsButton.onClick.AddListener(() => FormController.Instance.InstantiateMenu(MenusEnum.AudioSettings));

        _speechRecognitionManager.InitiateSpeechRecognition(_resultText, _recordButton, _stopButton, _recognitionStateImage);

        //  _recordButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.SignIn, ViewDirection.NONE));

        ShowQuestion();

       // FormController.Instance.ChangeFormLanguage();
    }

    public void VerifyAnswer(bool _correct)
    {
        if (_correct)
        {
            _answerText.text = "Correct Answer";
        }

        else
        {
            _answerText.text = "Incorrect Answer";
        }
    }

    public void ShowQuestion()
    {
        int _randomMinigameQuestion = Random.Range(0, DataManager.Instance.MinigamesQuestionsDictionary[MinigameEnum.M1].Count);

        MinigamesController.Instance.CurrentMinigameQuestion = DataManager.Instance.MinigamesQuestionsDictionary[MinigameEnum.M1][_randomMinigameQuestion];

        _questionText.text = MinigamesController.Instance.CurrentMinigameQuestion.Question;
    }
}
