using System;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    Action subscribedMethods;

    // Untraceable events dependencies if static event calls are allowed!

    // public static void RegisterListener(IGameEventListener listenerToAdd, string eventName) { GameEvent.GetEvent(eventName).RegisterListener(listenerToAdd); }
    // public static void UnregisterListener(IGameEventListener listenerToRemove, string eventName) { GameEvent.GetEvent(eventName).UnregisterListener(listenerToRemove); }
    // public static void RegisterListener(Action methodToAdd, string eventName) { GameEvent.GetEvent(eventName).RegisterListener(methodToAdd); }
    // public static void UnregisterListener(Action methodToRemove, string eventName) { GameEvent.GetEvent(eventName).UnregisterListener(methodToRemove); }
    // public static void Raise(string eventName) { GetEvent(eventName).Raise(); }

    public void RegisterListener(IGameEventListener listener) { RegisterListener(listener.OnEventRaised); }
    public void UnregisterListener(IGameEventListener listener) { UnregisterListener(listener.OnEventRaised); }
    public void RegisterListener(Action methodToAdd) { /*UnregisterListener(methodToAdd);*/ subscribedMethods += methodToAdd; }
    public void UnregisterListener(Action methodToRemove) { subscribedMethods -= methodToRemove; }
    public void Raise() { if (subscribedMethods != null) subscribedMethods.Invoke(); }

    // Nota sobre RegisterListener: como hacer -= a un delegate que no existe no da error, para evitar suscripciones duplicadas se puede obligar a hacer -= primero
    // Note about RegisterListener: duplicated subscriptions can be avoid by doing -= before the subscription

    //static GameEvent GetEvent(string eventName)
    //{
    //    GameEvent eventToReturn = Resources.Load<GameEvent>("Events/" + eventName);
    //    if (eventToReturn != null)
    //    {
    //        return eventToReturn;
    //    }
    //    else
    //    {
    //        Debug.LogError("GameEvent: Evento " + eventName + " no encontrado en Resources/Events/");
    //        return null;
    //    }
    //}

    public string ListenersToString()
    {
        string s = "";
        if (subscribedMethods != null)
        {
            int i = 0;
            foreach (Delegate del in subscribedMethods.GetInvocationList())
            {
                string className = del.Method.DeclaringType.Name;
                string methodName = del.Method.Name + "()";
                string gameObjectName = (del.Target as Component).gameObject.name;
                bool isGameEventListener = (del.Target is IGameEventListener);
                if (!isGameEventListener)
                    s += "Listener " + i + " in Game Object \"" + gameObjectName + "\":\n\t" + className + "." + methodName + "\n\n";
                else
                    s += "Listener " + i + " in Game Object \"" + gameObjectName + "\":\n\t" + className + " as IGameEventListener (OnEventRaised).\n\n";
                i++;
            }
        }
        return s;
    }
}