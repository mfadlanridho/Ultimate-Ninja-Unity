using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyConfiguration))]
public class EnemyConfigurationEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        EnemyConfiguration enemyConfiguration = target as EnemyConfiguration;

        // if (GUILayout.Button("Regenerate Enemy Names")) {
		// 	enemyConfiguration.RegenerateEnemyNames();
		// }
    }
}