using UnityEngine;

public class MeleeSwooshSound : MonoBehaviour {
    [SerializeField] AudioClip[] sounds;
    [SerializeField, Range(0, 1)] float volume = 1f;
    MeleeAttack meleeAttack;

    int curIndex;

    private void Start() {
        meleeAttack = GetComponent<MeleeAttack>();
        meleeAttack.OnAttack += PlaySound;
    } 

    void PlaySound() {
        AudioManager.Instance.Play(sounds[curIndex], volume, transform.position);
        curIndex = (curIndex + 1) % sounds.Length;
    }
}