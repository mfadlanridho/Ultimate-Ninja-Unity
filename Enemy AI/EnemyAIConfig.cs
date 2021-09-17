using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAIConfig")]
public class EnemyAIConfig : ScriptableObject {
    public float WaitDist = 2f;
    public float WaitTime = 2f; 

    [Header("Firearm Attributes")]
    public float MaxAngle = 60f;
    public int FireCount = 8;
}