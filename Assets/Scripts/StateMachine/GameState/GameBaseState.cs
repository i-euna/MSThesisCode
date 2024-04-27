using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseState : ScriptableObject
{
    public GameStates state;
    public GameBaseState nextState;

    public GameEventWithStr OnGameStateChanged;

    public virtual void Enter()
    {

    }

    public virtual void Process()
    {

    }

    public virtual void Exit()
    {
        OnGameStateChanged.InvokeEvent(nameof(nextState.state));
    }

    public enum GameStates { GameInit, GameRunning, GamePaused, GameOver }
}
