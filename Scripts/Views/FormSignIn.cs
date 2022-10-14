using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormSignIn : MonoBehaviour
{
    Button _signInButton;
    Button _signUpButton;
    Button _forgotPasswordButton;
    TMP_InputField _usernameInputField;
    TMP_InputField _passwordInputField;
    [SerializeField] Sprite _background;
    public Sprite Background { get => _background; set => _background = value; }
    public Button SignInButton { get => _signInButton; set => _signInButton = value; }
    public Button SignUpButton { get => _signUpButton; set => _signUpButton = value; }
    public TMP_InputField UsernameInputField { get => _usernameInputField; set => _usernameInputField = value; }
    public TMP_InputField PasswordInputField { get => _passwordInputField; set => _passwordInputField = value; }
    public Button ForgotPasswordButton { get => _forgotPasswordButton; set => _forgotPasswordButton = value; }

    private void Awake()
    {
        FormController.Instance.FormSignIn = this;

        InitializeComponent();
    }

    public void InitializeComponent()
    {
        SignInButton = transform.Find("SignInButton").GetComponent<Button>();
        SignUpButton = transform.Find("SignUpButton").GetComponent<Button>();
        ForgotPasswordButton = transform.Find("ForgotButton").GetComponent<Button>();
        UsernameInputField = transform.Find("UsernameInput").GetComponent<TMP_InputField>();
        PasswordInputField = transform.Find("PasswordInput").GetComponent<TMP_InputField>();

        SignInButton.onClick.AddListener(() => SignIn());
        SignUpButton.onClick.AddListener(() => FormController.Instance.ChangeView(ViewsEnum.SignUp));

        Debug.Log(DataManager.Instance.CurrentLanguage);
        FormController.Instance.ChangeFormLanguage();
    }


    public void SignIn()
    {
        DataManager.Instance.UserSignIn = true;
        FormController.Instance.ChangeView(ViewsEnum.Home);
    }
}
