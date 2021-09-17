using UnityEngine;

public class MeleeAttackAnimationEvent : MonoBehaviour {
    MeleeAttack meleeAttack;

    private void Start() {
        meleeAttack = GetComponent<MeleeAttack>();
    }

    void AttackFromUpperRight() {
        meleeAttack.Attack(AttackType.AttackFromUpperRight);
    }

    void AttackFromUpperLeft() {
        meleeAttack.Attack(AttackType.AttackFromUpperLeft);
    }

    void AttackFromTheLeft() {
        meleeAttack.Attack(AttackType.AttackFromTheLeft);
    }

    void BigAttack() {
        meleeAttack.Attack(AttackType.BigAttack);
    }

    void DashAttack() {
        meleeAttack.Attack(AttackType.DashAttack);
    }

    void AttackFromTheRight() {
        meleeAttack.Attack(AttackType.AttackFromTheRight);
    }

    void AttackFromFront() {
        meleeAttack.Attack(AttackType.AttackFromFront);
    }

    void SpinningSlash() {
        meleeAttack.Attack(AttackType.SpinningSlash);
    }
}