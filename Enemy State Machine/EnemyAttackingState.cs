using UnityEngine;

public class EnemyAttackingState : StateMachineBehaviour {
    bool init;
    EnemyStateMachine stateMachine;
    EnemyAttacks attacks;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            stateMachine = animator.GetComponent<EnemyStateMachine>();
            attacks = animator.GetComponent<EnemyAttacks>();
        }
        stateMachine.State = EnemyState.Attacking;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        attacks.ResetTriggers(animator);
    }
}