using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLineController : MonoBehaviour
{
    [SerializeField] List<GameObject> _grayLines = new List<GameObject>();
    [SerializeField] List<GameObject> _blueLines = new List<GameObject>();
    [SerializeField] int _currentIndex = 0;

    [SerializeField] GameObject _touch;
    [SerializeField] GameObject _touchFollower;
    [SerializeField] int _touchIndex = 0;
    [SerializeField] List<GameObject> _nextLine = new List<GameObject>();
    bool _touchInMovement = false;


    Touch _currentTouch;
    public int CurrentIndex { get => _currentIndex; set => _currentIndex = value; }

    private void Start()
    {
        GetLines();

        _nextLine.Add(_touch);

        _touchFollower.transform.position = new Vector3(_grayLines[0].transform.position.x, _grayLines[0].transform.position.y);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _currentTouch = Input.GetTouch(0);
        }

        if (_touchInMovement)
        {
            if (_touchIndex + 1 < _nextLine.Count)
            {
                if (_touch.transform.position.x != _nextLine[_touchIndex + 1].transform.position.x && _touch.transform.position.y != _nextLine[_touchIndex + 1].transform.position.y)
                {
                    _touch.transform.position = Vector2.MoveTowards(_touch.transform.position, _nextLine[_touchIndex + 1].transform.position, 6f * Time.deltaTime);
                }

                else
                {
                    _touchIndex++;

                    if (_touchIndex == _nextLine.Count)
                    {
                        Debug.Log("Perdiste 1");
                        MinigameController2.instance.AnswerState(false);
                        _touchInMovement = false;
                    }
                }
            }

            else
            {
                if (_touchIndex < _grayLines.Count - 1)
                {
                    Debug.Log("FAIL");
                    MinigameController2.instance.AnswerState(false);
                    _touchInMovement = false;
                }

                else
                {
                    Debug.Log("WIN");
                    MinigameController2.instance.AnswerState(true);
                    _touchInMovement = false;
                }
            }
        }
    }

    public void GetLines()
    {
        var _grayLinesObtained = transform.Find("GrayLine").GetComponentsInChildren<MinigameLineProperty>();
        int _indexLine = 0;

        foreach (var item in _grayLinesObtained)
        {
            _grayLines.Add(item.gameObject);
            item.GetComponent<MinigameLineProperty>().LineIndex = _indexLine;
            _indexLine++;
        }

        var _blueLinesObtained = transform.Find("BlueLine").GetComponentsInChildren<SpriteRenderer>();

        foreach (var item in _blueLinesObtained)
        {
            _blueLines.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }

    public void NextIndex()
    {
        _blueLines[_currentIndex].SetActive(true);
        _currentIndex++;

        if (_currentIndex < _grayLines.Count)
        {
            _nextLine.Add(_grayLines[_currentIndex]);
            _touchFollower.transform.position = new Vector3(_grayLines[_currentIndex].transform.position.x, _grayLines[_currentIndex].transform.position.y);
        }
    }

    public void ResetLinePath()
    {
        _currentIndex = 0;

        foreach (var item in _blueLines)
        {
            item.SetActive(false);
        }
    }

    public void VerifyLineAnswer()
    {
        _touchInMovement = true;
    }
}
