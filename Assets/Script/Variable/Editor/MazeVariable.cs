// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;
using Game.Events;

namespace Variable
{
    [CustomEditor(typeof(MazeVariable), editorForChildClasses: true)]
    public class MazeVariableEditor : Editor
    {
        private Maze.MazeEventType selection;

        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
            EditorGUILayout.Space();

            GUI.enabled = Application.isPlaying;

            MazeVariable data = (MazeVariable)target;
            selection = (Maze.MazeEventType)EditorGUILayout.EnumPopup("Example Enum", selection);

            GameEventData e = data.MazeEvent;

            if (GUILayout.Button("Raise")) {
                e.Raise(selection);
            }
        }
    }

}