using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AttackingState : StateMachineBehaviour {
    EnemyTarget enemyTarget;
    float speed = 5f;
    float duration;

    private void Awake() {
        duration = 1f / speed;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        enemyTarget = FindObjectOfType<EnemyTarget>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (enemyTarget == null) 
            return;
        
        bool changedState = PlayerStateMachine.Instance.SetState(PlayerState.Attacking);
        if (!changedState)
            return;
        
        Transform target = enemyTarget.CurrentTarget;
        TryLookAtTarget(animator.transform, target);
        MoveForward(animator.transform, target);
    }

    void TryLookAtTarget(Transform transform, Transform target) {
        if (target) {
            Vector3 targetPos = enemyTarget.CurrentTarget.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
        }
    }

    void MoveForward(Transform transform, Transform target) {
        if (PlayerStateMachine.Instance.Colliding)
                return;
        
        float dist = 1000;
        if (target) {
            dist = Vector3.SqrMagnitude(target.position - transform.position);
        }

        if (dist > 2) {
            transform.DOMove(transform.position + (transform.forward * .75f), duration );
        }
    }
}