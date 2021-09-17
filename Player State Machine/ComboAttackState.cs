using UnityEngine;

public class ComboAttackState : StateMachineBehaviour {
    ComboAttack comboAttack;
    bool initialized;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized) {
            initialized = true;
            comboAttack = animator.GetComponent<ComboAttack>();
        }
        if (stateInfo.IsTag("S")) {
            comboAttack.MoveCameraForward();
            comboAttack.DoCombo();
        }

        comboAttack.IncreaseAttackCount();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!animator.GetCurrentAnimatorStateInfo(layerIndex).IsTag("C")) {
            comboAttack.MoveCameraBack();
        }
    }
}