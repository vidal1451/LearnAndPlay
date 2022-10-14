using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenesEnum { MainMenu, M1, M2, M3, M4 }
public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject _mainCanvas;
    [SerializeField] ScenesEnum _currentScene;

    private void Awake()
    {
        InitializeScene();
    }

    public void InitializeScene()
    {
        FormController.Instance.SetElements(_mainCanvas);

        switch (_currentScene)
        {
            case ScenesEnum.MainMenu:
                if (!DataManager.Instance.UserSignIn)
                {
                    DataManager _currentDataManager = DataManager.Instance;
                    SoundController _currentSoundController = SoundController.Instance;

                    FormController.Instance.ChangeView(ViewsEnum.SignIn, ViewDirection.NONE);

                    UserData _simulatedUser = new UserData();
                    _simulatedUser.UserName = "Miguel Angel Ortega";
                    _simulatedUser.UserAge = 28;

                    DataManager.Instance.CurrentUser = _simulatedUser;

                    SoundController.Instance.PlayMusic(MusicEnum.Rainbow);
                    // DataManager.Instance.CurrentLanguage = "es_ES";
                }

                else
                {
                    FormController.Instance.ChangeView(ViewsEnum.Home, ViewDirection.NONE);
                }
                break;

            case ScenesEnum.M1:
                FormController.Instance.ChangeView(ViewsEnum.Minigame1Form, ViewDirection.NONE);
                break;

            case ScenesEnum.M2:
                FormController.Instance.ChangeView(ViewsEnum.Minigame2Form, ViewDirection.NONE);
                break;

            case ScenesEnum.M3:
                FormController.Instance.ChangeView(ViewsEnum.Minigame3Form, ViewDirection.NONE);
                break;

            case ScenesEnum.M4:
                FormController.Instance.ChangeView(ViewsEnum.Minigame4Form, ViewDirection.NONE);
                break;
        }
    }
}
