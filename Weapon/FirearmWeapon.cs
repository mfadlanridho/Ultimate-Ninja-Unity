using UnityEngine;

public class FirearmWeapon : MonoBehaviour {
    public float Damage {get; private set;}
    public Vector2 FireRate;
    public System.Action OnFire;

    public virtual void Fire() {
        if (OnFire != null)
            OnFire();
    }

    public void SetDamage(float damage) {
        this.Damage = damage;
    }
}