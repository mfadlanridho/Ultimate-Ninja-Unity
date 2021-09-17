using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    [SerializeField] Sound pauseSound;
    [SerializeField] Sound backToMenuSound;
    [SerializeField] Sound replaySound;
    bool paused;

    public void PauseUnpauseGame() {
        AudioManager.Instance.Play(pauseSound.Audio, pauseSound.Volume);
        paused = !paused;
        if (paused)
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;
    }

    public void BackToMainMenu() {
        AudioManager.Instance.Play(backToMenuSound.Audio, backToMenuSound.Volume);
        LevelLoader.Instance.LoadScene(0);
    }

    public void Replay() {
        AudioManager.Instance.Play(replaySound.Audio, replaySound.Volume);
        LevelLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}