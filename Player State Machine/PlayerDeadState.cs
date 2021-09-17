using UnityEngine;

public class PlayerDeadState : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerStateMachine.Instance.SetState(PlayerState.Dead);
    }
}