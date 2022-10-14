using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorItem : MonoBehaviour
{
    [SerializeField] string _valueToReceive;
    [SerializeField] bool _oneItem;
    [SerializeField] int _numberOfItems;

    public bool VerifyAvailableReception()
    {
        bool _canReceive = false;

        if (_oneItem)
        {
            if (_numberOfItems <= 0)
            {
                _canReceive = true;
                _numberOfItems++;
            }
        }

        else
        {
            _canReceive = true;
            _numberOfItems++;
        }

        return _canReceive;
    }

    public void ObjectOutput()
    {
        _numberOfItems--;
    }

    public string ValueToReceive { get => _valueToReceive; set => _valueToReceive = value; }
    public bool OneItem { get => _oneItem; set => _oneItem = value; }
    public int NumberOfItems { get => _numberOfItems; set => _numberOfItems = value; }
}
