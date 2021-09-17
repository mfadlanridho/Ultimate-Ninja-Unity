using UnityEngine;

public class LookAtPlayerState : StateMachineBehaviour {
    bool init;
    Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.transform.LookAt(player);
    }
}