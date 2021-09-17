using UnityEngine;

public class Burn : MonoBehaviour {
    CharacterBase character;
    [SerializeField] float damage = .1f;

    void Update() {
        character?.TakeDamage(damage, true);
    }

    public void SetCharacter(CharacterBase character) {
        this.character = character;
    }
}