using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {
    HitAnimationHandler hitAnimationHandler;
    [HideInInspector] public float MaxHealth = 100f;
    [HideInInspector] public float CurrentHealth;

    public System.Action OnDeath;
    public System.Action OnResetHealth;
    public System.Action<float, float> AfterHealthChange;
    public System.Action OnDamaged;
    
    protected virtual void Start() {
        hitAnimationHandler = GetComponent<HitAnimationHandler>();
    }

    public virtual void TakeDamage(float damage, bool onlyTakeDamage = false, AttackType attackType = AttackType.None, Vector3 direction = default(Vector3)) {
        if (Dead)
            return;

        if (!onlyTakeDamage && OnDamaged != null) {
            OnDamaged();
        }

        CurrentHealth -= damage;

        if (AfterHealthChange != null) {
            AfterHealthChange(CurrentHealth, MaxHealth);
        }
        
        if (CurrentHealth <= 0) {
            Die();
        } else {
            if (attackType != AttackType.None) {
                hitAnimationHandler?.ReceiveHit(attackType, direction);
            }
        }
    }

    protected virtual void Die() {
        if (OnDeath != null) {
            OnDeath();
        }
    }

    public bool Dead {
        get {
            return CurrentHealth <= 0;
        }
    }

    protected void ResetHealth() {
        CurrentHealth = MaxHealth;

        if (OnResetHealth != null) {
            OnResetHealth();
        }
    }

    public void SetMaxHealth(float newMaxHealth) {
        MaxHealth = newMaxHealth;
        ResetHealth();
    }
}