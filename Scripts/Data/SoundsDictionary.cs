using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SfxEnum { ButtonClicked, ButtonHover, CorrectAnswer, IncorrectAnswer }
public enum MusicEnum { Rainbow }
public static class SoundsDictionary
{
    static string _generalSfxClipsPath = "Sounds/Sfx/";
    static string _generalMusicClipsPath = "Sounds/Music/";

    public static Dictionary<SfxEnum, string> _sfxClipsPathDictionary = new Dictionary<SfxEnum, string>()
    {
        {SfxEnum.ButtonClicked, _generalSfxClipsPath+"ButtonClicked"},
        {SfxEnum.ButtonHover, _generalSfxClipsPath+"ButtonHover"},
        {SfxEnum.CorrectAnswer, _generalSfxClipsPath+"CorrectAnswer"},
        {SfxEnum.IncorrectAnswer, _generalSfxClipsPath+"IncorrectAnswer"}
    };

    public static Dictionary<MusicEnum, string> _musicClipsPathDictionary = new Dictionary<MusicEnum, string>()
    {
        {MusicEnum.Rainbow,  _generalMusicClipsPath+"Rainbow"},
    };

}