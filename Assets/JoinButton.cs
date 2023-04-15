using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(JoinButton))]
public class JoinButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RelayManager rm = (RelayManager)target;
        if (GUILayout.Button("Join Game as Client"))
        {
            rm.JoinRelay(rm.JoinCode);
        }
    }
}
