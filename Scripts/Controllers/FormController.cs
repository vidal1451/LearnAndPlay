using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;


public enum ViewDirection { LEFT, RIGHT, NONE }


public class FormController : Singleton<FormController>
{
    GameObject _mainCanvas;
    Animator _fade;

    GameObject _currentMenu;
    GameObject _currentViewObject;
    GameObject _lastViewObject;

    [SerializeField] ViewsEnum _currentView;
    [SerializeField] ViewsEnum _lastView;

    FormSignUp _formSignUp;
    FormSignIn _formSignIn;
    FormHome _formHome;
    FormMenu _formMenu;
    FormMinigame1 _formMinigame1;
    FormMinigame2 _formMinigame2;
    FormMinigame4 _formMinigame4;
    MenuAudioSettings _menuAudioSettings;

    Dictionary<Enumerators.LanguageCode, int> _languagesValueDictionary = new Dictionary<Enumerators.LanguageCode, int>();

    public FormSignIn FormSignIn { get => _formSignIn; set => _formSignIn = value; }
    public FormHome FormHome { get => _formHome; set => _formHome = value; }
    public FormSignUp FormSignUp { get => _formSignUp; set => _formSignUp = value; }
    public FormMenu FormMenu { get => _formMenu; set => _formMenu = value; }
    public FormMinigame1 FormMinigame1 { get => _formMinigame1; set => _formMinigame1 = value; }
    public Dictionary<Enumerators.LanguageCode, int> LanguagesValueDictionary { get => _languagesValueDictionary; set => _languagesValueDictionary = value; }
    public FormMinigame2 FormMinigame2 { get => _formMinigame2; set => _formMinigame2 = value; }
    public MenuAudioSettings MenuAudioSettings { get => _menuAudioSettings; set => _menuAudioSettings = value; }
    public FormMinigame4 FormMinigame4 { get => _formMinigame4; set => _formMinigame4 = value; }
    public GameObject MainCanvas { get => _mainCanvas; set => _mainCanvas = value; }

    private void Awake()
    {
        AddLanguages();
    }

    public void ChangeView(ViewsEnum _newView, ViewDirection _viewDirection = ViewDirection.LEFT)
    {
        StartCoroutine(ChangeViewCoroutine(_newView, _viewDirection));
    }

    public IEnumerator ChangeViewCoroutine(ViewsEnum _newView, ViewDirection _viewDirection = ViewDirection.LEFT)
    {

        if (_currentView != _newView || _currentViewObject == null)
        {
            _lastView = _currentView;
            _lastViewObject = _currentViewObject;

            _currentView = _newView;
            _currentViewObject = (GameObject)Instantiate(Resources.Load(ViewsDictionary._viewsPathDictionary[_newView]), _mainCanvas.transform.Find("Views").transform);

            if (_viewDirection == ViewDirection.NONE)
            {
                _currentViewObject.GetComponent<Animator>().Play("None");
            }
        }

        if (_lastViewObject != null)
        {
            _lastViewObject.GetComponent<Animator>().Play("Hide");
        }

        StartCoroutine(ChangeBackground(_newView));

        yield return new WaitForSeconds(0.5f);

        if (_lastViewObject != null)
        {
            Destroy(_lastViewObject);
        }
    }

    public void InstantiateMenu(MenusEnum _newMenu)
    {
        if (_currentMenu == null)
        {
            _currentMenu = (GameObject)Instantiate(Resources.Load(ViewsDictionary._menusPathDictionary[_newMenu]), _mainCanvas.transform.Find("Menus").transform);
        }
    }

    public void CloseMenu(float _hideDuration)
    {
        StartCoroutine(CloseMenuCoroutine(_hideDuration));
    }


    public IEnumerator CloseMenuCoroutine(float _hideDuration)
    {
        if (_currentMenu != null)
        {
            _currentMenu.GetComponent<Animator>().Play("Hide", -1, 0);
        }

        yield return new WaitForSeconds(_hideDuration);

        if (_currentMenu != null)
        {
            Destroy(_currentMenu);
        }
    }
    public IEnumerator ChangeBackground(ViewsEnum _newView)
    {
        Sprite _currentSpriteView = null;
        bool _existMenu = false;

        switch (_newView)
        {
            case ViewsEnum.SignIn:
                _currentSpriteView = _formSignIn.Background;
                _existMenu = true;
                break;
            case ViewsEnum.Home:
                _currentSpriteView = _formHome.Background;
                _existMenu = true;
                break;
            case ViewsEnum.SignUp:
                _currentSpriteView = _formSignUp.Background;
                _existMenu = true;
                break;
        }

        GameObject _backBackground = null;
        GameObject _frontBackground = null;

        if (_existMenu)
        {
            _backBackground = FindObjectOfType<Canvas>().transform.Find("BackgroundBack").gameObject;
            _frontBackground = FindObjectOfType<Canvas>().transform.Find("BackgroundFront").gameObject;
            _backBackground.GetComponent<Image>().sprite = _currentSpriteView;
            _frontBackground.GetComponent<Animator>().Play("Hide", -1, 0);
        }

        yield return new WaitForSeconds(0.5f);

        if (_existMenu)
        {
            _frontBackground.GetComponent<Image>().sprite = _backBackground.GetComponent<Image>().sprite;
            _frontBackground.GetComponent<Animator>().Play("Show", -1, 0);
        }
    }

    public void SetElements(GameObject _canvas)
    {
        _mainCanvas = _canvas;
        _fade = _canvas.transform.Find("Fade").GetComponent<Animator>();
    }


    public void AddLanguages()
    {
        //SPANISH ID: 1

        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_AR, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_BO, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_CL, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_CO, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_CR, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_DO, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_EC, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_ES, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_GT, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_HN, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_MX, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_NI, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_PA, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_PE, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_PR, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_PY, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_SV, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_US, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_UY, 1);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.es_VE, 1);

        //ENGLISH ID: 2

        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_AU, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_CA, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_GB, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_GH, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_IE, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_IN, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_KE, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_NG, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_NZ, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_PH, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_TZ, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_US, 2);
        LanguagesValueDictionary.Add(Enumerators.LanguageCode.en_ZA, 2);
    }
    public void ChangeLanguage(string _currentLanguage)
    {
        foreach (var item in _languagesValueDictionary)
        {
            if (item.Key.ToString() == _currentLanguage)
            {
                PlayerPrefs.SetString("CurrentLanguage", item.Key.ToString());
                DataManager.Instance.CurrentLanguage = item.Key.ToString();
                ChangeFormLanguage();
                return;
            }
        }
    }

    public void ChangeFormLanguage()
    {
        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            DataManager.Instance.CurrentLanguage = PlayerPrefs.GetString("CurrentLanguage");
        }

        int _languageId = 0;

        foreach (var item in _languagesValueDictionary)
        {
            if (item.Key.ToString() == DataManager.Instance.CurrentLanguage)
            {
                _languageId = item.Value;
            }
        }

        Debug.Log(DataManager.Instance.CurrentLanguage);

        switch (_languageId)
        {
            //CASE 1 GENERIC SPANISH
            case 1:
                switch (_currentView)
                {
                    case ViewsEnum.SignIn:
                        FormSignIn _currentFormSignIn = _formSignIn;
                        _currentFormSignIn.SignInButton.GetComponentInChildren<TextMeshProUGUI>().text = "Iniciar sesion";
                        _currentFormSignIn.SignUpButton.GetComponentInChildren<TextMeshProUGUI>().text = "Registrarse";
                        _currentFormSignIn.ForgotPasswordButton.GetComponentInChildren<TextMeshProUGUI>().text = "¿Olvidaste tu contraseña?";
                        _currentFormSignIn.UsernameInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Usuario";
                        _currentFormSignIn.PasswordInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Contraseña";
                        break;

                    case ViewsEnum.Home:
                        FormHome _currentFormHome = _formHome;
                        _currentFormHome.UserNameText.text = $"Nombre: {DataManager.Instance.CurrentUser.UserName}";
                        _currentFormHome.UserAgeText.text = $"Edad: {DataManager.Instance.CurrentUser.UserAge}";
                        break;

                    case ViewsEnum.SignUp:
                        break;

                    case ViewsEnum.Minigame1Form:
                        break;
                }
                break;

            //CASE 1 GENERIC SPANISH

            case 2:
                switch (_currentView)
                {
                    case ViewsEnum.SignIn:
                        FormSignIn _currentFormSignIn = _formSignIn;
                        _currentFormSignIn.SignInButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sign In";
                        _currentFormSignIn.SignUpButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sign Up";
                        _currentFormSignIn.ForgotPasswordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Forgot password?";
                        _currentFormSignIn.UsernameInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Username";
                        _currentFormSignIn.PasswordInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Password";
                        break;

                    case ViewsEnum.Home:
                        FormHome _currentFormHome = _formHome;
                        break;

                    case ViewsEnum.SignUp:
                        break;

                    case ViewsEnum.Minigame1Form:
                        break;
                }
                break;
        }

    }

    public void FadeState(bool _in)
    {
        if (_in)
        {
            _fade.Play("FadeIn", -1, 0);
        }

        else
        {
            _fade.Play("FadeOut", -1, 0);
        }
    }

}
