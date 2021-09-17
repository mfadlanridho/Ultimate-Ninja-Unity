using UnityEngine;

public class ChangeDirectionState : StateMachineBehaviour {
    [SerializeField] EnemyAIConfig config;
    bool init;
    Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        float angle = AngleToPlayer(animator.transform);
        if (angle >= config.MaxAngle) {
            animator.Play("Turn Right 90");
        }
        else if (angle <= -1 * config.MaxAngle) {
            animator.Play("Turn Left 90");
        }
    }

    float AngleToPlayer(Transform transform) {
        Vector3 pathToPlayer = player.position - transform.position;
        return Vector3.SignedAngle(transform.forward, pathToPlayer, Vector3.up);
    }
}