using UnityEngine;

public class FootstepSounds : MonoBehaviour {
    Sound[] sounds;
    [SerializeField] ReusableSound reusableSound;
    int soundIndex;

    private void Awake() {
        sounds = reusableSound.Sounds;
    }

    public void PlayFootstepSound() {
        soundIndex = (soundIndex + 1)%sounds.Length;
        Sound sound = sounds[soundIndex];
        AudioManager.Instance.Play(sound.Audio, sound.Volume, transform.position);
    }
}