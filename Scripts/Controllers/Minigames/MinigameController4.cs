using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameController4 : MonoBehaviour
{
    public static MinigameController4 instance;

    [SerializeField] int _minimalScoreToWin = 2;
    [SerializeField] int _maxRounds = 3;
    [SerializeField] int _currentRound = 0;
    [SerializeField] int _maxTries = 3;
    [SerializeField] int _currentTry = 0;
    [SerializeField] int _totalCorrectAnswers = 0;
    bool _winGame = false;

    [SerializeField] List<GameObject> _cards = new List<GameObject>();
    [SerializeField] List<GameObject> _letters = new List<GameObject>();
    [SerializeField] ReceptorItem _receptor;

    [SerializeField] List<bool> _answers = new List<bool>();

    [SerializeField] List<string> _lettersProbe = new List<string>();
    [SerializeField] List<int> _lettersIndexPassed = new List<int>();

    [SerializeField] string _currentLetter;

    [SerializeField] Animator _pet;

    public List<bool> Answers { get => _answers; set => _answers = value; }
    public bool WinGame { get => _winGame; set => _winGame = value; }
    public int TotalCorrectAnswers { get => _totalCorrectAnswers; set => _totalCorrectAnswers = value; }
    public int MaxRounds { get => _maxRounds; set => _maxRounds = value; }

    private void Start()
    {
        instance = this;
        DataManager _currDataManager = DataManager.Instance;

        NextLevel();
    }
    public void NextLevel()
    {
        if (_currentRound < MaxRounds)
        {
            _currentTry = 0;
            _currentRound++;
            Answers.Add(false);

            ResetLevel(true);
            GetRandomLetter();
            SetInfoForCards();
            GetRandomLetters();
        }

        else
        {
            ConfirmAnswers();
            FormController.Instance.InstantiateMenu(MenusEnum.WinMenu1);
        }
    }
    public void GetRandomLetter()
    {
        /*int _randomLetterIndex = Random.Range(0, DataManager.Instance.Letters.Count);

            _currentLetter = DataManager.Instance.Letters[_randomLetterIndex];*/

        for (int i = 0; i < 1; i++)
        {
            int _randomLetterIndex = Random.Range(0, _lettersProbe.Count);

            if (_lettersIndexPassed.Count < _lettersProbe.Count)
            {
                if (_lettersIndexPassed.Contains(_randomLetterIndex))
                {
                    i--;
                }

                else
                {
                    _lettersIndexPassed.Add(_randomLetterIndex);
                }
            }

            _currentLetter = _lettersProbe[_randomLetterIndex];
        }

        _receptor.ValueToReceive = _currentLetter.ToLower();
    }

    public void SetInfoForCards()
    {
        string _generalSpritePath = "Sprites/Minigames/M4/";

        for (int i = 0; i < _cards.Count; i++)
        {
            string _letterSpritePath = _generalSpritePath + _currentLetter + "/" + _currentLetter + (i + 1);
            _cards[i].transform.Find("Object").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_letterSpritePath);
            _cards[i].transform.Find("Name/Title").GetComponent<TextMeshProUGUI>().text = _currentLetter;
        }
    }

    public void GetRandomLetters()
    {
        List<string> _randomLetters = new List<string>();

        _randomLetters.Add(_currentLetter);

        for (int i = 0; i < _letters.Count - 1; i++)
        {
            int _randomIndex = Random.Range(0, DataManager.Instance.Letters.Count);

            if (_randomLetters.Contains(DataManager.Instance.Letters[_randomIndex]))
            {
                i--;
            }

            else
            {
                _randomLetters.Add(DataManager.Instance.Letters[_randomIndex]);
            }
        }


        //RandomizeLetterPositions

        List<int> _indexSelected = new List<int>();


        for (int i = 0; i < _randomLetters.Count; i++)
        {
            int _currentLetter = Random.Range(0, _randomLetters.Count);

            if (_indexSelected.Contains(_currentLetter))
            {
                i--;
            }

            else
            {
                _letters[i].GetComponent<DragabbleAlphabetLetter>().Value = _randomLetters[_currentLetter].ToLower();
                _letters[i].transform.Find("Background/Letter").GetComponent<TextMeshProUGUI>().text = _randomLetters[_currentLetter];
                _indexSelected.Add(_currentLetter);
            }
        }
    }

    public void ResetLevel(bool _nextLevel = false)
    {
        if (_currentTry < _maxTries - 1)
        {
            foreach (var item in _letters)
            {
                item.GetComponent<DragabbleAlphabetLetter>().TotalReset();
            }

            if (!_nextLevel)
            {
                _currentTry++;
            }
        }

        else
        {
            NextLevel();
        }
    }

    public void VerifyAnswer()
    {
        DragabbleAlphabetLetter _letterSelected = null;

        foreach (var item in _letters)
        {
            if (item.GetComponent<DragabbleAlphabetLetter>().CurrentReceptor != null)
            {
                _letterSelected = item.GetComponent<DragabbleAlphabetLetter>();
                break;
            }
        }

        if (_letterSelected != null)
        {
            if (_letterSelected.CorrectAnswer)
            {
                Answers[_currentRound - 1] = true;
                NextLevel();
                _pet.Play("CorrectAnswer");
                SoundController.Instance.PlaySFX(SfxEnum.CorrectAnswer);
            }

            else
            {
                ResetLevel();
                _pet.Play("IncorrectAnswer");
                SoundController.Instance.PlaySFX(SfxEnum.IncorrectAnswer);
            }
        }

        else
        {
            Debug.Log("No ha seleccionado ninguna letra");
        }
    }

    public void ConfirmAnswers()
    {
        TotalCorrectAnswers = 0;

        foreach (var item in Answers)
        {
            if (item)
            {
                TotalCorrectAnswers++;
            }
        }

        if (TotalCorrectAnswers >= _minimalScoreToWin)
        {
            _winGame = true;
        }
    }


}
