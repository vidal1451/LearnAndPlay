using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormMinigame4 : MonoBehaviour
{
    Button _audioSettingsButton;

    private void Awake()
    {
        FormController _currentController = FormController.Instance;

        FormController.Instance.FormMinigame4 = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _audioSettingsButton = transform.Find("AudioSettingsButton").GetComponent<Button>();

        _audioSettingsButton.onClick.AddListener(() => FormController.Instance.InstantiateMenu(MenusEnum.AudioSettings));
    }
}
