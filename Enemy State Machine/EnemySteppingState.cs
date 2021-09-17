using UnityEngine;

public class EnemySteppingState : StateMachineBehaviour {
    EnemyStepTowardsPlayerEvent stepTowardsPlayerEvent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stepTowardsPlayerEvent == null) {
            stepTowardsPlayerEvent = animator.GetComponent<EnemyStepTowardsPlayerEvent>();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        stepTowardsPlayerEvent.MakeSureStepReset();
    }
}