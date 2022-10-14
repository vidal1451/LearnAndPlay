using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMinigameWin1 : MonoBehaviour
{
    TextMeshProUGUI _title;
    TextMeshProUGUI _totalCorrectAnswers;
    Button _closeButton;
    Button _reloadButton;

    private void Awake()
    {
        InitializeComponent();
    }
    public void InitializeComponent()
    {
        _closeButton = transform.Find("Background/CloseButton").GetComponent<Button>();
        _reloadButton = transform.Find("Background/ReloadButton").GetComponent<Button>();
        _title = transform.Find("Background/Title").GetComponent<TextMeshProUGUI>();
        _totalCorrectAnswers = transform.Find("Background/TotalCorrectAnswers").GetComponent<TextMeshProUGUI>();

        _closeButton.onClick.AddListener(() => MinigamesController.Instance.ChangeScene("MainMenu"));
        _reloadButton.onClick.AddListener(() => MinigamesController.Instance.ChangeScene(SceneManager.GetActiveScene().name));

        GetGameInfo();
    }

    public void GetGameInfo()
    {
        if (MinigameController4.instance.WinGame)
        {
            _title.text = "WIN";
        }

        else
        {
            _title.text = "LOSE";
        }

        _totalCorrectAnswers.text = MinigameController4.instance.TotalCorrectAnswers.ToString() + "/" + MinigameController4.instance.MaxRounds;
    }
}
