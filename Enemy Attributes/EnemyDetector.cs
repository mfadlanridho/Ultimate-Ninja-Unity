using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyDetector : MonoBehaviour 
{
    [Header("Debug purpose"), SerializeField] bool findEnemiesAtStart;

    [Space]
    public List<Enemy> Enemies = new List<Enemy>();

    public Action OnFoundEnemy;
    public Action OnNoEnemy;
    public Action<Enemy> OnAddingEnemy;

    public Action<Enemy> OnBeforeRemoveEnemy;
    public Action OnAfterRemoveEnemy;

    private void Start() {
        if (findEnemiesAtStart) {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies) {
                Enemies.Add(enemy);
            }
        }
    }

    public void AddEnemy(Enemy enemy) {
        if (Enemies.Contains(enemy))
            return;   
        
        if (OnAddingEnemy != null)
            OnAddingEnemy(enemy);
        
        Enemies.Add(enemy);

        if (Enemies.Count == 1 && OnFoundEnemy != null) 
            OnFoundEnemy();
    }

    public void RemoveEnemy(Enemy enemy) {
        if (!Enemies.Contains(enemy))
            return;

        if (Enemies.Count == 1 && OnNoEnemy != null)
            OnNoEnemy();

        if (OnBeforeRemoveEnemy != null)
            OnBeforeRemoveEnemy(enemy);

        Enemies.Remove(enemy);

        if (OnAfterRemoveEnemy != null)
            OnAfterRemoveEnemy();
    }
}