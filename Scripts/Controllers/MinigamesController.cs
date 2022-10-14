using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesController : Singleton<MinigamesController>
{
    string _minigameId;
    MinigameQuestion _currentMinigameQuestion;

    public string MinigameId { get => _minigameId; set => _minigameId = value; }
    public MinigameQuestion CurrentMinigameQuestion { get => _currentMinigameQuestion; set => _currentMinigameQuestion = value; }


    public void PlayMinigame(string _currentMinigameId)
    {
        _minigameId = _currentMinigameId;

        ChangeScene(DataManager.Instance.MinigamesDictionary[_currentMinigameId].SceneName);
    }

    public void ChangeScene(string _sceneName)
    {
        if (!string.IsNullOrEmpty(_minigameId)) { _minigameId = ""; }

        StartCoroutine(ChangeSceneCoroutine(_sceneName));
    }

    public IEnumerator ChangeSceneCoroutine(string _sceneName)
    {
        FormController.Instance.FadeState(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_sceneName);
    }

}
