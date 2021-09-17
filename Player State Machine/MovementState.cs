using UnityEngine;

public class MovementState : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerStateMachine.Instance.SetState(PlayerState.None);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Vector3 input = new Vector3(VirtualInputManager.Instance.GetHorizontal(), 0, VirtualInputManager.Instance.GetVertical());
        animator.SetFloat("Magnitude", input.magnitude);
        
        LookTowardsDirection(animator.transform ,input);
    }

    void LookTowardsDirection(Transform transform, Vector3 direction) {
        transform.LookAt(transform.position + direction);
    }

}