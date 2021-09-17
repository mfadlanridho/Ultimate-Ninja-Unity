using UnityEngine;

public class FirearmProjectileHit : MonoBehaviour {
    [SerializeField] GameObject hit;
    FirearmProjectile projectile;

    private void Start() {
        projectile = GetComponent<FirearmProjectile>();
        projectile.OnHit += GenerateEffect;
    }

    void GenerateEffect(Vector3 position) {
        Instantiate(hit, position, Quaternion.identity);
    }
}