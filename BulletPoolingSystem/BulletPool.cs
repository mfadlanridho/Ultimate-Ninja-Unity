using UnityEngine;
using System.Collections.Generic;

namespace BulletPoolingSystem {
public class BulletPool : MonoBehaviour {
    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    private void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    [SerializeField] Bullet prefab;
    [SerializeField] BulletHit hitEffectPrefab;
    Queue<Bullet> bullets = new Queue<Bullet>();
    Queue<BulletHit> hitEFfects = new Queue<BulletHit>();

    public void Spawn(Vector3 position, Quaternion rotation, float damage) {
        if (bullets.Count == 0) {
            Instantiate(prefab, position, rotation).SetDamage(damage);
        } else {
            Bullet bullet = bullets.Dequeue();
            bullet.transform.SetPositionAndRotation(position, rotation);
            bullet.gameObject.SetActive(true);
        }
    }

    public void EmitHitEffect(Vector3 position) {
        if (hitEFfects.Count == 0) {
            Instantiate(hitEffectPrefab.gameObject, position, Quaternion.identity);
        } else {
            BulletHit hitEffect = hitEFfects.Dequeue();
            hitEffect.transform.position = position;
            hitEffect.gameObject.SetActive(true);
        }
    }

    public void StoreToPool(Bullet bullet) {
        bullets.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
    }

    public void StoreToPool(BulletHit hitEffect) {
        hitEFfects.Enqueue(hitEffect);
        hitEffect.gameObject.SetActive(false);
    }
}
}