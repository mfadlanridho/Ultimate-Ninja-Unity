using UnityEngine;

public class EnemyShootingState : StateMachineBehaviour {    
    bool init;
    EnemyStateMachine stateMachine;
    FirearmAttack weapon;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if( !init) {
            init = true;
            stateMachine = animator.GetComponent<EnemyStateMachine>();
            weapon = animator.GetComponent<FirearmAttack>();
        }
        stateMachine.State = EnemyState.Shooting;

        if (stateMachine.State == EnemyState.Shooting)
            weapon.Fire();
    }
}