using UnityEngine;

namespace BulletPoolingSystem {
public class Bullet : MonoBehaviour {
    Collider[] hit = new Collider[1];
    float lifeTime;
    float damage;

    private void OnEnable() {
        hit[0] = null;
        lifeTime = 2f;
    }

    private void Update() {
        lifeTime -= Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 20f);

        Physics.OverlapSphereNonAlloc(transform.position, 1f, hit, LayerMask.GetMask("Player", "Enemy"));
        if (hit[0] != null) {
            // Debug.Log(hit[0].name + " with damage " + damage);
            hit[0].GetComponent<CharacterBase>().TakeDamage(damage);

            BulletPool.Instance.StoreToPool(this);
            BulletPool.Instance.EmitHitEffect(transform.position);
        } else if (lifeTime <= 0) {
            BulletPool.Instance.StoreToPool(this);
        }
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
}