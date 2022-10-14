using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlphabetDictionaries : MonoBehaviour
{
    public enum Letters { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z }

    private void Awake()
    {
        string[] _letterNames = Enum.GetNames(typeof(Letters));

        for (int i = 0; i < _letterNames.Length; i++)
        {
            DataManager.Instance.Letters.Add(_letterNames[i]);
        }

        string _letters = "";

        foreach (var item in DataManager.Instance.Letters)
        {
            _letters += item + ", ";
        }
    }
}
