using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController2 : MonoBehaviour
{
    public static MinigameController2 instance;

    [SerializeField] List<GameObject> _linePaths = new List<GameObject>();
    [SerializeField] List<int> _linePathCompleted = new List<int>();

    GameObject _currentPath;

    private void Start()
    {
        instance = this;

        FormController.Instance.ChangeView(ViewsEnum.Minigame2Form, ViewDirection.LEFT);

        SelectRandomLinePath();
    }

    public void SelectRandomLinePath()
    {
        Debug.Log("Hola");

        foreach (var item in _linePaths)
        {
            item.GetComponent<MinigameLineController>().ResetLinePath();
        }

        foreach (var item in _linePaths)
        {
            item.SetActive(false);
        }

        int _newLinePathIndex = 0;

        for (int i = 0; i < 1; i++)
        {
            _newLinePathIndex = Random.Range(0, _linePaths.Count);

            if (_linePathCompleted.Count < _linePaths.Count)
            {
                if (_linePathCompleted.Contains(_newLinePathIndex))
                {
                    i--;
                }

                else
                {
                    _linePathCompleted.Add(_newLinePathIndex);
                }
            }
        }


        _currentPath = _linePaths[_newLinePathIndex];
        _linePaths[_newLinePathIndex].SetActive(true);
    }

    public void VerifyLineAnswer()
    {
        _currentPath.GetComponent<MinigameLineController>().VerifyLineAnswer();
    }

    public void AnswerState(bool _win)
    {
        if (_win)
        {
            FormController.Instance.FormMinigame2.VerifyAnswer(true);
        }

        else
        {
            FormController.Instance.FormMinigame2.VerifyAnswer(false);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
