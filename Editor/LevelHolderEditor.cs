using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelHolder))]
public class LevelHolderEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        LevelHolder levelHolder = target as LevelHolder;

        if (GUILayout.Button("Find Levels In Resources")) {
			levelHolder.FindLevelsInResources();
		}

        if (GUILayout.Button("Find Buttons In Children")) {
			levelHolder.FindButtonsInChildren();
		}

        if (GUILayout.Button("Create Buttons In Children")) {
			levelHolder.CreateButtonsInChildren();
		}
    }
}