using UnityEngine;

public class HitAnimationHandler : MonoBehaviour {
    Animator animator;
    CharacterBase character;
    bool isPlayer;

    private void Start() {
        isPlayer = transform.CompareTag("Player");
        animator = GetComponent<Animator>();

        character = GetComponent<CharacterBase>();
        character.OnDeath += PlayDeathAnimation;
    }

    public void ReceiveHit(AttackType type, Vector3 direction = default(Vector3)) {
        if (character == null)
            return;
        
        if (character.Dead)
            return;
        
        if (isPlayer && (PlayerStateMachine.Instance.State == PlayerState.Rolling || PlayerStateMachine.Instance.State == PlayerState.Reviving))
            return;

        
        
        if (direction != default(Vector3))
            transform.LookAt(transform.position - direction);
        
        PlayHitAnimation(type);
    }

    void PlayDeathAnimation() {
        animator.Play("Death");
    }

    void PlayHitAnimation(AttackType type) {
        animator.Play("HitBy " + type.ToString());
    }
}