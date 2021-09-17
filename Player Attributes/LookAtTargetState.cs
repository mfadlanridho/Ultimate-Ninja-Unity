using UnityEngine;

public class LookAtTargetState : StateMachineBehaviour {
    bool init;
    EnemyTarget enemyTarget;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            enemyTarget = FindObjectOfType<EnemyTarget>();
        }
        TryLookAtTarget(animator.transform);
    }

    void TryLookAtTarget(Transform transform) {
        if (enemyTarget.CurrentTarget) {
            Vector3 target = enemyTarget.CurrentTarget.position;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}