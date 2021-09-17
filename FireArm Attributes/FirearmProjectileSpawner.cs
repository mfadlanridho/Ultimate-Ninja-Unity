using UnityEngine;

public class FirearmProjectileSpawner : MonoBehaviour {
    [SerializeField] Transform muzzle;
    [SerializeField] FirearmProjectile projectile;

    FirearmWeapon weapon;

    private void Start() {
        weapon = GetComponent<FirearmWeapon>();
        weapon.OnFire += SpawnProjectile;
    }

    void SpawnProjectile() {
        FirearmProjectile spawned = Instantiate(projectile.gameObject, muzzle.position, muzzle.rotation).GetComponent<FirearmProjectile>();
        spawned.SetDamage(weapon.Damage);
    }
}