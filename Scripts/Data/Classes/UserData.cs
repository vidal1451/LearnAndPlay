using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    string _userName;
    int _userAge;

    public string UserName { get => _userName; set => _userName = value; }
    public int UserAge { get => _userAge; set => _userAge = value; }
}
