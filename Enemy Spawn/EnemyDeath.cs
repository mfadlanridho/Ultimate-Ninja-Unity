using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {
    public void DeactivateEnemy(Enemy enemy) {
        StartCoroutine(Deactivate(enemy));
    }

    IEnumerator Deactivate(Enemy enemy) {
        yield return new WaitForSeconds(5f);
        
        enemy.InGame = false;
        enemy.gameObject.SetActive(false);;
    }
}