using UnityEngine;

public class RollingState : StateMachineBehaviour {
    bool initialized;
    float speed = 10f;    
    Roll roll;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerStateMachine.Instance.SetState(PlayerState.Rolling);
        if (!initialized) {
            initialized = true;
            roll = animator.GetComponent<Roll>();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Vector3 input = new Vector3(VirtualInputManager.Instance.GetHorizontal(), 0, VirtualInputManager.Instance.GetVertical());
        animator.SetFloat("Magnitude", input.magnitude);
        LookTowardsDirection(animator.transform ,input);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        // make sure the rolling stop if the state exits
        roll.RollingStop();
    }

    void LookTowardsDirection(Transform transform, Vector3 direction) {
        transform.LookAt(transform.position + direction);
    }
}