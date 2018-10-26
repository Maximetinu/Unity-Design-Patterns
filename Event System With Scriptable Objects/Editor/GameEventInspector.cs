using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameEvents
{

    [CustomEditor(typeof(GameEvent))]
    public class GameEventInspector : Editor
    {

        GameEvent thisEvent;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            thisEvent = target as GameEvent;

            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button("Raise event"))
            {
                thisEvent.Raise();
            }
            GUILayout.Label("Listeners:", EditorStyles.boldLabel);
            if (Application.isPlaying)
            {
                GUILayout.Space(10);
                GUILayout.Label(thisEvent.ListenersToString());
            }
            else
            {
                GUILayout.Label("Only in play mode");
            }
        }
    }
}