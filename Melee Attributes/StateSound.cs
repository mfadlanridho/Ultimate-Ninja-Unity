using UnityEngine;

public class StateSound : StateMachineBehaviour {
    [SerializeField] Sound[] sounds;
    [SerializeField] ReusableSound reusableSound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (reusableSound) {
            foreach (Sound sound in reusableSound.Sounds) {
                AudioManager.Instance.Play(sound.Audio, sound.Volume, animator.transform.position);
            }
        }
        else {
            foreach (Sound sound in sounds) {
                AudioManager.Instance.Play(sound.Audio, sound.Volume, animator.transform.position);
            }
        }
    }
}