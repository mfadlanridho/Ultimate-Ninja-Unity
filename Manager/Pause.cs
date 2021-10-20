using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    [SerializeField] Sound pauseSound;
    [SerializeField] Sound backToMenuSound;
    [SerializeField] Sound replaySound;

    [Space]
    [SerializeField] AudioSource bgMusic;
    [SerializeField] AudioSource ambientSound;
    [SerializeField] GameObject musicOnTick;

    bool paused;

    private void Start() {
        SetMusicDisabled(PlayerStats.Instance.MusicDisabled);
    }

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
        GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Movement");
    }

    void SetMusicDisabled(bool disabled) {
        bgMusic.gameObject.SetActive(!disabled);
        musicOnTick.SetActive(!disabled);

        if (disabled) {
            ambientSound.volume = .2f;
        } else {
            ambientSound.volume = .25f;
        }

        PlayerStats.Instance.SetMusicDisabled(disabled);
    }

    public void MusicOnOff() {
        SetMusicDisabled(!PlayerStats.Instance.MusicDisabled);
    }
}