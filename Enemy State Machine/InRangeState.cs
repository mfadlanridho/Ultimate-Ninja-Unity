using UnityEngine;

public class InRangeState : StateMachineBehaviour {
    EnemyStateMachine stateMachine;
    AIMoveToFormation ai;
    EnemyAttacks attacks;
    bool initialized;
    float timePassed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !initialized) {
            initialized = true;
            stateMachine = animator.GetComponent<EnemyStateMachine>();
            ai = animator.GetComponent<AIMoveToFormation>();
            attacks = animator.GetComponent<EnemyAttacks>();
        }
        attacks.ResetTriggers(animator);
        stateMachine.State = EnemyState.InRange;
        timePassed = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (NotInFormationPosition(animator.transform.position) && !ai.EdgedByPlayer()) {
            animator.SetBool("InRange", false);
        } else if (TimeToAttack()) {
            attacks.Attack(animator);
        }
    }

    bool NotInFormationPosition(Vector3 curPosition) {
        return Vector3.SqrMagnitude(ai.FormationTargetPosition - curPosition) > 1f;
    }

    bool TimeToAttack() {
        timePassed += Time.deltaTime;
        return timePassed >= ai.Config.WaitTime;
    }
}