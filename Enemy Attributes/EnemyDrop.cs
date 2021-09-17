using UnityEngine;

public class EnemyDrop : MonoBehaviour {
    [SerializeField] GameObject drop;

    void Start() {
        GetComponent<Enemy>().OnDeath += Drop;
    }

    void Drop() {
        int rand = Random.Range(0, 11);
        if (rand <= 3)
            Instantiate(drop, transform.position, Quaternion.identity);
    }
}