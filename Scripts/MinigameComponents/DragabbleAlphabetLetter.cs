using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragabbleAlphabetLetter : MonoBehaviour
{
    [SerializeField] ReceptorItem _currentReceptor;
    [SerializeField] string _value;
    [SerializeField] bool _correctAnswer;
    RaycastHit _hit;
    bool _canMove = false;
    [SerializeField] bool _scaleEffect = true;
    Vector3 _initialPosition;
    Vector3 _initialScale;

    public bool CanMove { get => _canMove; set => _canMove = value; }
    public string Value { get => _value; set => _value = value; }
    public ReceptorItem CurrentReceptor { get => _currentReceptor; set => _currentReceptor = value; }
    public bool CorrectAnswer { get => _correctAnswer; set => _correctAnswer = value; }

    private void Start()
    {
        _initialPosition = transform.localPosition;
        _initialScale = transform.localScale;

        ResetScale(false);
    }

    private void Update()
    {
        if (_canMove)
        {
            if (Input.touchCount > 0)
            {
                Touch _currentTouch = Input.GetTouch(0);
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(_currentTouch.position).x, Camera.main.ScreenToWorldPoint(_currentTouch.position).y, transform.position.z);

                Physics.Raycast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50), out _hit);
            }
        }
    }

    public void InitializeDrag()
    {
        _canMove = true;

        ResetScale(true);
        ReceptorExit();
    }

    public void VerifyDrop()
    {
        if (_hit.collider != null)
        {
            if (_hit.collider.GetComponent<ReceptorItem>())
            {
                if (_hit.collider.GetComponent<ReceptorItem>().VerifyAvailableReception())
                {
                    CurrentReceptor = _hit.collider.GetComponent<ReceptorItem>();
                    transform.position = new Vector3(_hit.collider.transform.position.x, _hit.collider.transform.position.y, transform.position.z);
                    VerifyAnswer();
                }

                else
                {
                    TotalReset();
                }
            }
        }

        else
        {
            ReceptorExit();
            TotalReset();
        }

        _canMove = false;
    }

    public void ReceptorExit()
    {
        if (CurrentReceptor != null)
        {
            CurrentReceptor.ObjectOutput();
            CorrectAnswer = false;
            CurrentReceptor = null;
        }
    }

    public void TotalReset()
    {
        transform.localPosition = _initialPosition;
        ResetScale(false);
        ReceptorExit();
    }

    public void ResetScale(bool _bigScale)
    {
        if (_scaleEffect)
        {
            if (_bigScale)
            {
                transform.localScale = _initialScale;
            }

            else
            {
                transform.localScale = _initialScale * 0.7f;
            }
        }
    }


    public void VerifyAnswer()
    {
        if (Value == CurrentReceptor.ValueToReceive)
        {
            CorrectAnswer = true;
        }

        else
        {
            CorrectAnswer = false;
        }
    }

}
