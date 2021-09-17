using UnityEngine;

public class LevelLoaderEndState : StateMachineBehaviour {
    [SerializeField] Sound endSound;
    
    bool animationEnd;
    bool audioPlayed;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        audioPlayed = false;
        animationEnd = false;    
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!audioPlayed && stateInfo.normalizedTime >= .05f) {
            AudioManager.Instance.Play(endSound.Audio, endSound.Volume);
            audioPlayed = true;
        }
        
        if (!animationEnd && stateInfo.normalizedTime >= .99f) {
            LevelLoader.Instance.SetAnimating(false);
            animationEnd = true;
        }
    }
}