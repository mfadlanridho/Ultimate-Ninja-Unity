using UnityEngine;

public class BasicAttackState : StateMachineBehaviour {
    BasicAttack basicAttackMono;
    bool initialized;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ResetTrigger(animator);

        if (!initialized) {
            initialized = true;
            basicAttackMono = animator.GetComponent<BasicAttack>();
        }
        basicAttackMono.IncreaseAttackCount();
    }

    void ResetTrigger(Animator animator) {
        animator.SetBool("ContinueBasicAttack", false);
    }
}