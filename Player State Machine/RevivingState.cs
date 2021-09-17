using UnityEngine;

public class RevivingState : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerStateMachine.Instance.SetState(PlayerState.Reviving);
    }
}