using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectViewer : MonoBehaviour
{
    [SerializeField] Vector3 _objectScale;
    [SerializeField] Vector3 _valueScaled;
    [SerializeField] Vector3 _valueToScale;
    [SerializeField] float _value;

    private void Start()
    {
        _objectScale = transform.localScale;
    }
    void Update()
    {
        _value = Camera.main.aspect;

        IdentifyAspectRatio(_value);
        gameObject.transform.localScale = _valueScaled;
    }

    public void IdentifyAspectRatio(float _aspectValue)
    {
        if (_aspectValue >= 1.99) //18:9
        {
            _valueToScale = new Vector3(1.12f, 1, 1);
        }

        else if (_aspectValue >= 1.77) //19:9
        {
            _valueToScale = new Vector3(1, 1, 1);
        }

        else if (_aspectValue >= 1.33) //4:3
        {
            _valueToScale = new Vector3(0.73f, 0.98f, 1);
        }

        _valueScaled = new Vector3(_objectScale.x * _valueToScale.x, _objectScale.y * _valueToScale.y, _objectScale.z * _valueToScale.z);
    }
}
//18:9 X= 1.12 Y= 1
//19:9 X= 1 Y= 1
//4:3 X= 0.73 Y= 0.98



//18:9 1.99
//19:9 1.77
//4:3 1.33