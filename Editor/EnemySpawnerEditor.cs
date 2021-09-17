using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        EnemySpawner enemySpawner = target as EnemySpawner;

        if (GUILayout.Button("Find Enemies In Resources")) {
			enemySpawner.FindEnemiesInResources();
		}
    }
}