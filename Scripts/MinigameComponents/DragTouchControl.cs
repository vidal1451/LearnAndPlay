using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTouchControl : MonoBehaviour
{
    Touch _touch1;
    [SerializeField] GameObject _currentDraggableObject;


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch1 = Input.GetTouch(0);

            Ray _ray = Camera.main.ScreenPointToRay(_touch1.position);
            RaycastHit _hit;
            Physics.Raycast(_ray, out _hit);

            if (_touch1.phase == TouchPhase.Began)
            {
                if (_hit.collider != null)
                {
                    if (_hit.collider.gameObject.GetComponent<DragabbleAlphabetLetter>())
                    {
                        _currentDraggableObject = _hit.collider.gameObject;
                        _hit.collider.gameObject.GetComponent<DragabbleAlphabetLetter>().InitializeDrag();
                    }

                    if (_hit.collider.gameObject.GetComponent<DragabbleColor>())
                    {
                        _currentDraggableObject = _hit.collider.gameObject;
                        _hit.collider.gameObject.GetComponent<DragabbleColor>().InitializeDrag();
                    }
                }
            }

            if (_touch1.phase == TouchPhase.Ended)
            {
                if (_currentDraggableObject != null)
                {
                    ResetObject();
                }
            }

        }
    }


    public void ResetObject()
    {
        if (_currentDraggableObject != null)
        {
            if (_currentDraggableObject.GetComponent<DragabbleAlphabetLetter>())
            {
                _currentDraggableObject.GetComponent<DragabbleAlphabetLetter>().VerifyDrop();
            }

            if (_currentDraggableObject.GetComponent<DragabbleColor>())
            {
                _currentDraggableObject.GetComponent<DragabbleColor>().VerifyDrop();
            }

            _currentDraggableObject = null;
        }
    }

}
