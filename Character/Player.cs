using UnityEngine;

public class Player : CharacterBase {
    public System.Action AfterHealthIncrease;
    public System.Action<float, float> AfterHealthRestore;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void Start() {
        base.Start();

        if (PlayerStats.Instance != null)
            SetMaxHealth(PlayerStats.Instance.MaxHealth);
    }
    
    public void IncreaseMaxHealth(float value) {
        MaxHealth += value;
        if (AfterHealthIncrease != null) {
            AfterHealthIncrease();
        }
    }

    public override void TakeDamage(float damage, bool onlyTakeDamage = false, AttackType attackType = AttackType.None, Vector3 direction = default(Vector3)) { 
        if (PlayerStateMachine.Instance.State == PlayerState.Reviving) {
            return;
        }

        base.TakeDamage(damage, onlyTakeDamage, attackType, direction);
    }


    public void RestoreHealth(float value) {
        CurrentHealth += value;
        if (CurrentHealth > MaxHealth) {
            CurrentHealth = MaxHealth;
        }
        
        if (AfterHealthRestore != null) {
            AfterHealthRestore(CurrentHealth, MaxHealth);
        }
    }

    private void FixedUpdate() {
        PlayerStateMachine.Instance.Colliding = DetectCollision(transform.position, "Boundaries");

        if (PlayerStateMachine.Instance.Colliding) {
            Vector3 rightDir = transform.position + Quaternion.Euler(0, 30, 0) * transform.forward;
            Vector3 leftDir = transform.position + Quaternion.Euler(0, -30, 0) * transform.forward;

            PlayerStateMachine.Instance.Colliding = DetectCollision(rightDir, "Boundaries") && DetectCollision(leftDir, "Boundaries");
        }
    }
    
    Collider[] hitColliders = new Collider[1];
    protected bool DetectCollision(Vector3 position, string additionalMask = null) {
        int mask = additionalMask == null ? LayerMask.GetMask("Edge", "Props") 
            : LayerMask.GetMask("Edge", "Props", additionalMask);
        
        hitColliders[0] = null;
        Physics.OverlapSphereNonAlloc(position, 1f, hitColliders, mask);

        if (hitColliders[0] != null) {
            return true;
        }
        return false;
    }

    public void Revive() {
        Animator animator = GetComponent<Animator>();

        RestoreHealth(10000);
        animator.Play("Revive");
    }
}