using UnityEngine;

public class FirearmSound : MonoBehaviour {
    [SerializeField] AudioClip sound;
    [SerializeField, Range(0, 1)] float volume = 1f;
    FirearmWeapon weapon;

    private void Start() {
        weapon = GetComponent<FirearmWeapon>();
        weapon.OnFire += PlaySound;
    }

    void PlaySound() {
        AudioManager.Instance.Play(sound, volume, transform.position);
    }
}