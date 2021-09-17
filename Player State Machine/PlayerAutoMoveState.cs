using UnityEngine;

public class PlayerAutoMoveState : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetFloat("Magnitude", 1);
        
        LookTowardsDirection(animator.transform, Vector3.right);
    }

    void LookTowardsDirection(Transform transform, Vector3 direction) {
        transform.LookAt(transform.position + direction);
    }
}