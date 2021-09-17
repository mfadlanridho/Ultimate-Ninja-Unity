using UnityEngine;

public class RifleEnemyWaitMeleeAttackState : StateMachineBehaviour {
    [SerializeField] float attackDist = 1.5f;
    bool init;
    Transform player;

    float sqrDist;

    private void Awake() {
        sqrDist = attackDist * attackDist;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!init) {
            init = true;
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (IsInRange(animator.transform)) {
            animator.Play("Melee Attack");
        }
    }

    bool IsInRange(Transform transform) {
        return Vector3.SqrMagnitude(player.position - transform.position) <= sqrDist;
    }
}