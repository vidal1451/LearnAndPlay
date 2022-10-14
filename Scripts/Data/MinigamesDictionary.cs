using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame
{
    string _gameId;
    string _minigameName;
    string _thumbnailPath;
    string _sceneName;
    int _recommendedAge;


    public string MinigameName { get => _minigameName; set => _minigameName = value; }
    public string ThumbnailPath { get => _thumbnailPath; set => _thumbnailPath = value; }
    public string SceneName { get => _sceneName; set => _sceneName = value; }
    public int RecommendedAge { get => _recommendedAge; set => _recommendedAge = value; }
    public string GameId { get => _gameId; set => _gameId = value; }
}

public class MinigamesDictionary : MonoBehaviour
{
    private void Awake()
    {
        Minigame _minigame1 = new Minigame();
        _minigame1.GameId = "M1";
        _minigame1.MinigameName = "CrabGame";
        _minigame1.ThumbnailPath = "M1";
        _minigame1.SceneName = "M1";
        _minigame1.RecommendedAge = 5;

        Minigame _minigame2 = new Minigame();
        _minigame2.GameId = "M2";
        _minigame2.MinigameName = "BearGame";
        _minigame2.ThumbnailPath = "M2";
        _minigame2.SceneName = "M2";
        _minigame2.RecommendedAge = 3;

        Minigame _minigame3 = new Minigame();
        _minigame3.GameId = "M3";
        _minigame3.MinigameName = "OwlGame";
        _minigame3.ThumbnailPath = "M3";
        _minigame3.SceneName = "M4";
        _minigame3.RecommendedAge = 3;

        DataManager.Instance.MinigamesDictionary.Add(_minigame1.GameId, _minigame1);
        DataManager.Instance.MinigamesDictionary.Add(_minigame2.GameId, _minigame2);
        DataManager.Instance.MinigamesDictionary.Add(_minigame3.GameId, _minigame3);
    }

}
