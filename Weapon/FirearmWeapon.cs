using UnityEngine;

public class FirearmWeapon : Weapon {
    [Space]
    public Vector2 FireRate;
    public System.Action OnFire;

    public virtual void Fire() {
        if (OnFire != null)
            OnFire();
    }
}