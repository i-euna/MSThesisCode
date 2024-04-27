using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Custom/Event/GameEventWithStr")]
public class GameEventWithStr : ScriptableObject
{
    public UnityEvent<string> Event;

    public void InvokeEvent(string str)
    {
        Event.Invoke(str);
    }
}
