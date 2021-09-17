using UnityEngine;

public class NonRootMotionOneDirectionalMovementState : StateMachineBehaviour {
    [SerializeField] float overiddenSpeed;
    float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        PlayerStateMachine.Instance.SetState(PlayerState.None);
        speed = PlayerStats.Instance != null ? PlayerStats.Instance.Speed : 2f;
        speed = overiddenSpeed == 0 ? speed : overiddenSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Vector3 input = new Vector3(VirtualInputManager.Instance.GetHorizontal(), 0, VirtualInputManager.Instance.GetVertical());
        float magnitude = input.magnitude;
        animator.SetFloat("Magnitude", magnitude);
        if (magnitude > 0) {
            LookTowardsDirection(animator.transform ,input);
            MoveForward(animator.transform);
        }
    }

    void LookTowardsDirection(Transform transform, Vector3 direction) {
        transform.LookAt(transform.position + direction);
    }

    void MoveForward(Transform transform) {
        if (PlayerStateMachine.Instance.Colliding)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
    }

}