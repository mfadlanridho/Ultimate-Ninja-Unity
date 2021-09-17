using UnityEngine;

public abstract class AttackBase : MonoBehaviour {
    protected Animator animator;
    public System.Action OnAttack;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
    }

    protected virtual void Attack(string attackName, float damage) {
        if (OnAttack != null) 
            OnAttack();
        animator.Play(attackName);
    }
}