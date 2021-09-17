using UnityEngine;
using UnityEngine.UI;

public class BasicAttack : AttackBase {
    [SerializeField] string[] firstAttackNames;
    
    int attackCount;

    protected override void Start() {
        base.Start();
    }

    void InitiateAttack(int index) {
        if ( PlayerStateMachine.Instance.State == PlayerState.None ) {
            FirstAttack(index);
        }
        else if (PlayerStateMachine.Instance.State == PlayerState.Attacking) {
            if (attackCount < PlayerStats.Instance.BasicAttackCount)
                animator.SetBool("ContinueBasicAttack", true);
        }
    }

    void FirstAttack(int index) {
        attackCount = 0;
        Attack(firstAttackNames[index], 1f);
    }

    public void InitiateAttack() {
        InitiateAttack(0);
    }

    public void IncreaseAttackCount() {
        attackCount++;
    }
}