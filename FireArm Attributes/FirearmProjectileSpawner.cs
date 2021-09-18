using UnityEngine;
using BulletPoolingSystem;

public class FirearmProjectileSpawner : MonoBehaviour {
    [SerializeField] Transform muzzle;
    [SerializeField] FirearmProjectile projectile;

    FirearmWeapon weapon;

    private void Start() {
        weapon = GetComponent<FirearmWeapon>();
        weapon.OnFire += SpawnProjectile;
    }

    void SpawnProjectile() {
        BulletPool.Instance.Spawn(muzzle.position, muzzle.rotation, weapon.Damage);
    }
}