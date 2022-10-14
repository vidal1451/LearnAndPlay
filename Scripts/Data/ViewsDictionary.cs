using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewsEnum { SignIn, Home, SignUp, Minigame1Form, Minigame2Form, Minigame3Form, Minigame4Form }
public enum MenusEnum { AudioSettings, WinMenu1 }
public static class ViewsDictionary
{
    static string _generalViewsPath = "Prefabs/Views/";
    static string _generalMenusPath = "Prefabs/Menus/";

    public static Dictionary<ViewsEnum, string> _viewsPathDictionary = new Dictionary<ViewsEnum, string>()
    {
        {ViewsEnum.SignIn, _generalViewsPath+"SignIn"},
        {ViewsEnum.Home, _generalViewsPath+"Home"},
        {ViewsEnum.SignUp, _generalViewsPath+"SignUp"},
        {ViewsEnum.Minigame1Form, _generalViewsPath+"Minigame1Form" },
        {ViewsEnum.Minigame2Form, _generalViewsPath+"Minigame2Form" },
        {ViewsEnum.Minigame3Form, _generalViewsPath+"Minigame3Form" },
        {ViewsEnum.Minigame4Form, _generalViewsPath+"Minigame4Form" }
    };

    public static Dictionary<MenusEnum, string> _menusPathDictionary = new Dictionary<MenusEnum, string>()
    {
        {MenusEnum.AudioSettings, _generalMenusPath+"AudioSettings"},
        {MenusEnum.WinMenu1, _generalMenusPath+"GameOverMinigameMenu1"}
    };

}
