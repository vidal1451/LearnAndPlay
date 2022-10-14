using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLineProperty : MonoBehaviour
{
    [SerializeField] MinigameLineController _lineController;
    [SerializeField] int _lineIndex;

    public int LineIndex { get => _lineIndex; set => _lineIndex = value; }

    private void Start()
    {
        Debug.Log(gameObject.transform.parent.parent.name);
        _lineController = gameObject.transform.parent.parent.GetComponent<MinigameLineController>();
    }

    private void Update()
    {
        if (_lineController.CurrentIndex == _lineIndex)
        {
            if (Input.touchCount > 0)
            {
                if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)) < 1)
                {
                    _lineController.NextIndex();
                }
            }
        }
    }
}
