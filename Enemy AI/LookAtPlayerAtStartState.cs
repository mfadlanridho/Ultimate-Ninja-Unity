using UnityEngine;

public class LookAtPlayerAtStartState : StateMachineBehaviour {
    bool init;
    Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            player = GameObject.FindWithTag("Player").transform;
        }
        animator.transform.LookAt(player);
    }
}