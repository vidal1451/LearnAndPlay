using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[Serializable]
public class ButtonSoundState
{
    public List<EventTriggerType> _eventTypes = new List<EventTriggerType>();
    public List<SfxEnum> _effectClips = new List<SfxEnum>();

    public ButtonSoundState() { }


    public ButtonSoundState(List<SfxEnum> effectClips, List<EventTriggerType> eventTypes)
    {
        _effectClips = effectClips;
        _eventTypes = eventTypes;
    }

}
