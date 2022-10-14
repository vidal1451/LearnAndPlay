using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    string _speechRecognitionApiKey = "AIzaSyA1ICuMLt-6-wD1LLOoiwoSWqPNkCXU1Tc";
    string _currentLanguage;

    bool _userSignIn = false;
    UserData _currentUser = new UserData();
    Dictionary<string, Minigame> _minigamesDictionary = new Dictionary<string, Minigame>();
    Dictionary<MinigameEnum, List<MinigameQuestion>> _minigamesQuestionsDictionary = new Dictionary<MinigameEnum, List<MinigameQuestion>>();
    List<string> _letters = new List<string>();

    public Dictionary<string, Minigame> MinigamesDictionary { get => _minigamesDictionary; set => _minigamesDictionary = value; }
    public UserData CurrentUser { get => _currentUser; set => _currentUser = value; }
    public Dictionary<MinigameEnum, List<MinigameQuestion>> MinigamesQuestionsDictionary { get => _minigamesQuestionsDictionary; set => _minigamesQuestionsDictionary = value; }
    public string SpeechRecognitionApiKey { get => _speechRecognitionApiKey; set => _speechRecognitionApiKey = value; }
    public string CurrentLanguage { get => _currentLanguage; set => _currentLanguage = value; }
    public List<string> Letters { get => _letters; set => _letters = value; }
    public bool UserSignIn { get => _userSignIn; set => _userSignIn = value; }

    private void Awake()
    {
        gameObject.AddComponent<MinigamesDictionary>();
        gameObject.AddComponent<MinigamesQuestionsDictionary>();
        gameObject.AddComponent<AlphabetDictionaries>();
    }
}
