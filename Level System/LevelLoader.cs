using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    [SerializeField] Animator transition;
    
    public System.Action OnAnimationEnd;
    public bool Animating {get; private set;}

    # region Singleton
    public static LevelLoader Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    # endregion

    public void LoadScene(int index) {
        StartCoroutine(InitiateSceneChange(index));
    }

    IEnumerator InitiateSceneChange(int index) {

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        PlayerStateMachine.Instance.ResetState();
        SceneManager.LoadScene(index);
        transition.SetTrigger("End");

        // if going to menu
        if (index == 0) {
            // AdManager.Instance?.ShowInterstitialAd();
        }
    }

    public void SetAnimating(bool animating) {
        Animating = animating;

        if (animating == false) {
            if (OnAnimationEnd != null)
                OnAnimationEnd();
        }
    }
}