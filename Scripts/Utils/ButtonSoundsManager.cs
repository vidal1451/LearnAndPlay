using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundsManager : MonoBehaviour
{
    public List<ButtonSoundState> _buttonStateSounds = new List<ButtonSoundState>();

    [SerializeField] List<Button> _buttons = new List<Button>();
    [SerializeField] List<int> _buttonsStates = new List<int>();

    private void Start()
    {
        if (_buttons.Count > 0)
        {
            AddButtonSounds();
        }
    }

    public void AddButtonSounds()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            EventTrigger _currentEventTrigger = _buttons[i].gameObject.AddComponent<EventTrigger>();

            for (int a = 0; a < _buttonStateSounds[_buttonsStates[i]]._eventTypes.Count; a++)
            {
                EventTrigger.Entry _currentEntry = new EventTrigger.Entry();
                _currentEntry.eventID = _buttonStateSounds[_buttonsStates[i]]._eventTypes[a];
                SfxEnum _currentClip = _buttonStateSounds[_buttonsStates[i]]._effectClips[a];
                _currentEntry.callback.AddListener(delegate { SoundController.Instance.PlaySFX(_currentClip, false); });
                _currentEventTrigger.triggers.Add(_currentEntry);
            }
        }
    }


    public void AddButtonSound(Button _button, int _buttonStateIndex)
    {
        EventTrigger _currentEventTrigger = _button.gameObject.AddComponent<EventTrigger>();

        for (int a = 0; a < _buttonStateSounds[_buttonStateIndex]._eventTypes.Count; a++)
        {
            EventTrigger.Entry _currentEntry = new EventTrigger.Entry();
            _currentEntry.eventID = _buttonStateSounds[_buttonStateIndex]._eventTypes[a];
            SfxEnum _currentClip = _buttonStateSounds[_buttonStateIndex]._effectClips[a];
            _currentEntry.callback.AddListener(delegate { SoundController.Instance.PlaySFX(_currentClip, false); });
            _currentEventTrigger.triggers.Add(_currentEntry);
        }
    }
}
