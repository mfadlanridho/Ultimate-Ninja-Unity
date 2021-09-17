using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapSpawner))]
public class MapSpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        MapSpawner mapSpawner = target as MapSpawner;

        if (GUILayout.Button("Generate Map")) {
			mapSpawner.GenerateMap();
		}

        if (GUILayout.Button("Find Trap Floors")) {
			mapSpawner.FindTrapFloorsInResources();
		}
    }
}