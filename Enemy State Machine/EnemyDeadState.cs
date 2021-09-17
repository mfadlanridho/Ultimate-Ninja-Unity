using UnityEngine;

public class EnemyDeadState : StateMachineBehaviour {
    EnemyStateMachine stateMachine;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( stateMachine == null) {
            stateMachine = animator.GetComponent<EnemyStateMachine>();
        }
        stateMachine.State = EnemyState.Dead;
    }
}