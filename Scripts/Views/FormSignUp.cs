using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormSignUp : MonoBehaviour
{
    Button _signUpButton;
    Button _signInButton;
    TMP_InputField _usernameInputField;
    TMP_InputField _nameInputField;
    TMP_InputField _passwordInputField;
    [SerializeField] Sprite _background;
    public Sprite Background { get => _background; set => _background = value; }
    private void Awake()
    {
        FormController.Instance.FormSignUp = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        _signUpButton = transform.Find("SignUpButton").GetComponent<Button>();
        _signInButton = transform.Find("SignInButton").GetComponent<Button>();
        _usernameInputField = transform.Find("UsernameInput").GetComponent<TMP_InputField>();
        _nameInputField = transform.Find("NameInput").GetComponent<TMP_InputField>();
        _passwordInputField = transform.Find("PasswordInput").GetComponent<TMP_InputField>();

        _signInButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.SignIn));
        _signUpButton.onClick.AddListener(() => SignUp());

        FormController.Instance.ChangeFormLanguage();
    }

    public void SignUp()
    {
        DataManager.Instance.UserSignIn = true;
        FormController.Instance.ChangeView(ViewsEnum.Home);
    }
}
