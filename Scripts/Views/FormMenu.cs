using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormMenu : MonoBehaviour
{
    Button _backButton;
    Button _signOutButton;
    Button _profileButton;

    private void Awake()
    {
        FormController.Instance.FormMenu = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _backButton = transform.Find("BackButton").GetComponent<Button>();
        _signOutButton = transform.Find("MenuBackground/SignOutButton").GetComponent<Button>();
        _profileButton = transform.Find("MenuBackground/ProfileButton").GetComponent<Button>();

        _backButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.SignIn));
        _signOutButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.Home));
        _profileButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.Home));

        FormController.Instance.ChangeFormLanguage();
    }

}
