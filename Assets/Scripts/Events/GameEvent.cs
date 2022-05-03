using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    private static HashSet<GameEvent> listenedEvents = new HashSet<GameEvent>();
    private HashSet<GameEventListener> gameEventListeners = new HashSet<GameEventListener>();

    public void Register(GameEventListener gameEventListener)
    {
        gameEventListeners.Add(gameEventListener);
        listenedEvents.Add(this);
    }

    public void Deregister(GameEventListener gameEventListener)
    {
        gameEventListeners.Remove(gameEventListener);
        if (gameEventListeners.Count == 0)
            listenedEvents.Remove(this);
    }

    public void Invoke()
    {
        foreach (var gameEventListener in gameEventListeners)
        {
            gameEventListener.RaiseEvent();
        }
    }

    public static void RaiseEvent(string eventName)
    {
        foreach (var gameEvent in listenedEvents)
        {
            if(gameEvent.name == eventName)
            {
                gameEvent.Invoke();
            }
        }
    }
}
