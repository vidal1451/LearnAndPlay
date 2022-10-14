using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController1 : MonoBehaviour
{

    void Start()
    {
        FormController.Instance.ChangeView(ViewsEnum.Minigame1Form, ViewDirection.NONE);
    }

}
