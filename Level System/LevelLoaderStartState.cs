using UnityEngine;

public class LevelLoaderStartState : StateMachineBehaviour {
    [SerializeField] Sound startSound;

    bool audioPlayed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LevelLoader.Instance.SetAnimating(true);
        audioPlayed = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!audioPlayed && stateInfo.normalizedTime >= .01f) {
            AudioManager.Instance.Play(startSound.Audio, startSound.Volume);
            audioPlayed = true;
        }
    }
}