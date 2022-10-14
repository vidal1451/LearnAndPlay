using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinigameEnum { M1, M2 }

public class MinigameQuestion
{
    string _question;
    List<string> _options = new List<string>();
    string _correctAnswer;
    MinigameEnum _minigameQuestionEnum;

    public string Question { get => _question; set => _question = value; }
    public List<string> Options { get => _options; set => _options = value; }
    public string CorrectAnswer { get => _correctAnswer; set => _correctAnswer = value; }
    public MinigameEnum MinigameQuestionEnum { get => _minigameQuestionEnum; set => _minigameQuestionEnum = value; }
}

public class MinigamesQuestionsDictionary : MonoBehaviour
{
    private void Awake()
    {
        MinigameQuestion _minigameQuestion1 = new MinigameQuestion();
        _minigameQuestion1.Question = "Record: How are you?";
        _minigameQuestion1.Options = new List<string>() { { "" }, { "" } };
        _minigameQuestion1.MinigameQuestionEnum = MinigameEnum.M1;
        _minigameQuestion1.CorrectAnswer = "how are you?";

        MinigameQuestion _minigameQuestion2 = new MinigameQuestion();
        _minigameQuestion2.Question = "Record: Hello";
        _minigameQuestion2.Options = new List<string>() { { "" }, { "" } };
        _minigameQuestion2.MinigameQuestionEnum = MinigameEnum.M1;
        _minigameQuestion2.CorrectAnswer = "hello";

        DataManager.Instance.MinigamesQuestionsDictionary.Add(MinigameEnum.M1, new List<MinigameQuestion>());
        DataManager.Instance.MinigamesQuestionsDictionary.Add(MinigameEnum.M2, new List<MinigameQuestion>());

        DataManager.Instance.MinigamesQuestionsDictionary[MinigameEnum.M1].Add(_minigameQuestion1);
        DataManager.Instance.MinigamesQuestionsDictionary[MinigameEnum.M1].Add(_minigameQuestion2);
    }

}
