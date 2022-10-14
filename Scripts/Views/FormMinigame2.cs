using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormMinigame2 : MonoBehaviour
{
    TextMeshProUGUI _answerText;


    private void Awake()
    {
        FormController.Instance.FormMinigame2 = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _answerText = transform.Find("AnswerText").GetComponent<TextMeshProUGUI>();
    }

    public void VerifyAnswer(bool _correct)
    {
        if (_correct)
        {
            _answerText.text = "WIN";
        }

        else
        {
            _answerText.text = "FAIL";
        }
    }
}
