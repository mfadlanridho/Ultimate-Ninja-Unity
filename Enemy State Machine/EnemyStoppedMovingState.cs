using UnityEngine;

public class EnemyStoppedMovingState : StateMachineBehaviour {
    bool init;
    EnemyStateMachine stateMachine;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if( !init) {
            init = true;
            stateMachine = animator.GetComponent<EnemyStateMachine>();
        }    
        stateMachine.State = EnemyState.Stopped;
    }
}