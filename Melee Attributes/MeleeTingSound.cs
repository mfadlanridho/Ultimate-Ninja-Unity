using UnityEngine;

public class MeleeTingSound : MonoBehaviour {
    [SerializeField] ReusableSound sounds;

    MeleeAttack meleeAttack;
    int curIndex;

    private void Start() {
        meleeAttack = GetComponent<MeleeAttack>();
        meleeAttack.OnAttack += PlaySound;
    } 

    void PlaySound() {
        AudioManager.Instance.Play(sounds.Sounds[curIndex].Audio, sounds.Sounds[curIndex].Volume, transform.position);
        curIndex = (curIndex + 1) % sounds.Sounds.Length;
    }

}