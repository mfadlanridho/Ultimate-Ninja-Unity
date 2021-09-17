using UnityEngine;

public class ShootingStart : StateMachineBehaviour {
    FirearmAttack weapon;
    bool init;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ( !init) {
            init = true;
            weapon = animator.GetComponent<FirearmAttack>();
        }
        weapon.ResetFireCount();
    }
}