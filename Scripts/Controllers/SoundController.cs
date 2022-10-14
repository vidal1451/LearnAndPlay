using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField]
    int _currentSoundManager = 0;

    [SerializeField] AudioSource _musicSource;
    [SerializeField] List<AudioSource> _sfxSourcesList = new List<AudioSource>();

    [SerializeField] float _musicVolumenValue = 1;
    [SerializeField] float _sfxVolumenValue = 1;

    [SerializeField] bool _musicMuted;
    [SerializeField] bool _sfxMuted;

    public float MusicVolumenValue { get => _musicVolumenValue; set => _musicVolumenValue = value; }
    public float SfxVolumenValue { get => _sfxVolumenValue; set => _sfxVolumenValue = value; }
    public bool MusicMuted { get => _musicMuted; set => _musicMuted = value; }
    public bool SfxMuted { get => _sfxMuted; set => _sfxMuted = value; }

    private void Awake()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();

        _sfxSourcesList.Add(gameObject.AddComponent<AudioSource>());
        _sfxSourcesList.Add(gameObject.AddComponent<AudioSource>());
        _sfxSourcesList.Add(gameObject.AddComponent<AudioSource>());

    }

    public void PlayMusic(MusicEnum _clip)
    {
        if (_musicSource == null)
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
        }

        _musicSource.loop = true;

        _musicSource.clip = Resources.Load<AudioClip>(SoundsDictionary._musicClipsPathDictionary[_clip]);

        _musicSource.Play();
    }

    public void PlaySFX(SfxEnum _clip, bool _stop = false)
    {
        if (_stop)
        {
            StopSFX(true);
        }

        _sfxSourcesList[_currentSoundManager].loop = false;

        _sfxSourcesList[_currentSoundManager].clip = Resources.Load<AudioClip>(SoundsDictionary._sfxClipsPathDictionary[_clip]);

        _sfxSourcesList[_currentSoundManager].Play();

        if (_currentSoundManager >= _sfxSourcesList.Count - 1)
        {
            _currentSoundManager = 0;
        }

        else
        {
            _currentSoundManager++;
        }
    }

    public void StopSFX(bool _all = false)
    {
        if (_all)
        {
            foreach (var item in _sfxSourcesList)
            {
                item.Stop();
            }
        }

        else
        {
            if (_currentSoundManager - 1 < 0)
            {
                _sfxSourcesList[_sfxSourcesList.Count - 1].Stop();
            }

            else
            {
                _sfxSourcesList[_currentSoundManager - 1].Stop();
            }
        }
    }

    public void ChangeSfxVolumen(float _value, bool _mute = false)
    {
        if (!_sfxMuted)
        {
            _sfxVolumenValue = _value;
        }

        foreach (var item in _sfxSourcesList)
        {
            item.volume = _value;
        }
    }

    public void ChangeMusicVolumen(float _value, bool _mute = false)
    {
        if (!_musicMuted)
        {
            _musicVolumenValue = _value;
        }

        _musicSource.volume = _value;
    }

    public void MuteMusic()
    {
        if (MusicMuted)
        {
            MusicMuted = false;

            _musicSource.mute = false;
            _musicSource.volume = _musicVolumenValue;
        }

        else
        {
            MusicMuted = true;

            _musicSource.mute = true;
        }
    }

    public void MuteSFX()
    {
        if (SfxMuted)
        {
            SfxMuted = false;

            foreach (var item in _sfxSourcesList)
            {
                item.mute = false;
                item.volume = _sfxVolumenValue;
            }
        }

        else
        {
            SfxMuted = true;

            foreach (var item in _sfxSourcesList)
            {
                item.mute = true;
            }
        }

    }

}
