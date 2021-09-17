using UnityEngine;
using UnityEngine.UI;

public class AllButtonHandler : MonoBehaviour {
    [SerializeField] Sound sound;
    [SerializeField] Button[] buttons;

    private void Start() {
        foreach (var b in buttons) {
            b.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound() {
        AudioManager.Instance.Play(sound.Audio, sound.Volume);
    }
}