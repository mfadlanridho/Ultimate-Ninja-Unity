using UnityEngine;

public class RunAttack : AttackBase {
    [SerializeField] string attackName;
    bool isUnlocked;

    public void InitiateAttack() {
        if ( PlayerStateMachine.Instance.State == PlayerState.None ) {
            Attack(attackName, 1f);
        }
        else if ( PlayerStateMachine.Instance.State == PlayerState.Attacking) {
            animator.SetBool("ContinueBasicAttack", true);
        }
    }
}