using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class SpeechRecognitionManager : MonoBehaviour
{
    private GCSpeechRecognition _speechRecognition;

    private Image _speechRecognitionState;

    private Button _startRecordButton,
                   _stopRecordButton;

    //  private InputField _commandsInputField;

    private TextMeshProUGUI _resultText;

    // private Dropdown _languageDropdown;

    string _speechRecognitionPrefabPath = "SpeechConfig/GCSpeechRecognition";

    public GCSpeechRecognition SpeechRecognition { get => _speechRecognition; set => _speechRecognition = value; }

    public void InitiateSpeechRecognition(TextMeshProUGUI _textObject, Button _startRecordObject, Button _stopRecordObject, Image _recognitionStateObject)
    {
        Instantiate(Resources.Load(_speechRecognitionPrefabPath));
        SpeechRecognition = GCSpeechRecognition.Instance;
        SpeechRecognition.apiKey = DataManager.Instance.SpeechRecognitionApiKey;
        SpeechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
        SpeechRecognition.RecognizeFailedEvent += RecognizeFailedEventHandler;

        SpeechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;
        SpeechRecognition.StartedRecordEvent += StartedRecordEventHandler;
        SpeechRecognition.RecordFailedEvent += RecordFailedEventHandler;

        SpeechRecognition.EndTalkigEvent += EndTalkigEventHandler;

        _startRecordButton = _startRecordObject;
        _stopRecordButton = _stopRecordObject;

        _speechRecognitionState = _recognitionStateObject;

        _resultText = _textObject;

        //  _languageDropdown = _languageDropdownObject;

        _startRecordButton.onClick.AddListener(StartRecordButtonOnClickHandler);
        _stopRecordButton.onClick.AddListener(StopRecordButtonOnClickHandler);

        _startRecordButton.interactable = true;
        _stopRecordButton.interactable = false;
        _speechRecognitionState.color = Color.yellow;

        //  _languageDropdown.ClearOptions();

        /*  for (int i = 0; i < Enum.GetNames(typeof(Enumerators.LanguageCode)).Length; i++)
          {
              _languageDropdown.options.Add(new Dropdown.OptionData(((Enumerators.LanguageCode)i).Parse()));
          }*/

        // _languageDropdown.value = _languageDropdown.options.IndexOf(_languageDropdown.options.Find(x => x.text == Enumerators.LanguageCode.es_CO.Parse()));

        _speechRecognition.RequestMicrophonePermission(null);




        // select first microphone device
        if (_speechRecognition.HasConnectedMicrophoneDevices())
        {
            _speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[0]);
        }
    }

    private void OnDestroy()
    {
        SpeechRecognition.RecognizeSuccessEvent -= RecognizeSuccessEventHandler;
        SpeechRecognition.RecognizeFailedEvent -= RecognizeFailedEventHandler;

        SpeechRecognition.FinishedRecordEvent -= FinishedRecordEventHandler;
        SpeechRecognition.StartedRecordEvent -= StartedRecordEventHandler;
        SpeechRecognition.RecordFailedEvent -= RecordFailedEventHandler;

        SpeechRecognition.EndTalkigEvent -= EndTalkigEventHandler;
    }

    private void StartRecordButtonOnClickHandler()
    {
        _startRecordButton.interactable = false;
        _stopRecordButton.interactable = true;
        _resultText.text = string.Empty;

        SpeechRecognition.StartRecord(false);
    }

    private void StopRecordButtonOnClickHandler()
    {
        _stopRecordButton.interactable = false;
        _startRecordButton.interactable = true;

        SpeechRecognition.StopRecord();
    }

    private void StartedRecordEventHandler()
    {
        _speechRecognitionState.color = Color.red;
    }

    private void RecordFailedEventHandler()
    {
        _speechRecognitionState.color = Color.yellow;
        _resultText.text = "<color=red>Start record Failed. Please check microphone device and try again.</color>";

        _stopRecordButton.interactable = false;
        _startRecordButton.interactable = true;
    }

    private void EndTalkigEventHandler(AudioClip clip, float[] raw)
    {
        FinishedRecordEventHandler(clip, raw);
    }

    private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
    {
        if (_startRecordButton.interactable)
        {
            _speechRecognitionState.color = Color.yellow;
        }

        if (clip == null)
            return;

        RecognitionConfig config = RecognitionConfig.GetDefault();
        //config.languageCode = ((Enumerators.LanguageCode)_languageDropdown.value).Parse();
        config.languageCode = DataManager.Instance.CurrentLanguage;
        config.audioChannelCount = clip.channels;
        // configure other parameters of the config if need

        GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
        {
            audio = new RecognitionAudioContent()
            {
                content = raw.ToBase64()
            },
            //audio = new RecognitionAudioUri() // for Google Cloud Storage object
            //{
            //	uri = "gs://bucketName/object_name"
            //},
            config = config
        };

        SpeechRecognition.Recognize(recognitionRequest);
    }

    private void RecognizeFailedEventHandler(string error)
    {
        _resultText.text = "Recognize Failed: " + error;
    }

    private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
    {
        bool _correctAnswer = false;

        _resultText.text = "Detected: ";

        foreach (var result in recognitionResponse.results)
        {
            foreach (var alternative in result.alternatives)
            {
                // string _currentAlternative =;
                //  Debug.Log(EliminateIntonations(alternative.transcript.ToLowerInvariant().TrimEnd(' ').TrimStart(' ')));
                //  Debug.Log(alternative.transcript.ToLowerInvariant().TrimEnd(' ').TrimStart(' '));
                _resultText.text += "\n" + EliminateIntonations(alternative.transcript.ToLowerInvariant().TrimEnd(' ').TrimStart(' '));

                if (MinigamesController.Instance.CurrentMinigameQuestion != null)
                {
                    if (EliminateIntonations(alternative.transcript.ToLowerInvariant().TrimEnd(' ').TrimStart(' ').Replace(".", "")) == MinigamesController.Instance.CurrentMinigameQuestion.CorrectAnswer)
                    {
                        _correctAnswer = true;
                    }
                }
            }
        }

        if (MinigamesController.Instance.CurrentMinigameQuestion != null)
        {
            if (_correctAnswer)
            {
                Debug.Log("Respuesta correcta");

                FormController.Instance.FormMinigame1.VerifyAnswer(true);
                FormController.Instance.FormMinigame1.ShowQuestion();
            }

            else
            {
                FormController.Instance.FormMinigame1.VerifyAnswer(false);
                Debug.Log("Respuesta incorrecta");
            }
        }

    }

    public string EliminateIntonations(string _currentText)
    {
        string _currentAlternative = "";

        foreach (var item in _currentText)
        {
            if (item == 'á') { _currentAlternative += "a"; }
            else if (item == 'é') { _currentAlternative += "e"; }
            else if (item == 'í') { _currentAlternative += "i"; }
            else if (item == 'ó') { _currentAlternative += "o"; }
            else if (item == 'ú') { _currentAlternative += "u"; }
            else { _currentAlternative += item; }
        }

        return _currentAlternative;
    }


}
