using UnityEngine;
using DG.Tweening;

public class PopUpTrap : Trap {
    [SerializeField] Transform spikes;
    [SerializeField] float hiddenPos;
    [SerializeField] float poppedPos;
    [SerializeField] Transform hitPosition;
    [SerializeField] Sound sound;

    [Space]
    [SerializeField] Ease ease;
    [SerializeField] float delay = 0f;
    [SerializeField] float time = 3f;
    [SerializeField] float popDuration = 1f;
    [SerializeField] float hideDuration = 1f;

    [Space]
    [SerializeField] float damage = 30f;

    Collider[] hitColliders;

    protected override void Activate() {
        base.Activate();
        PlaySpikesTrap();
    }

    void PlaySpikesTrap() {
        DOTween.Sequence().SetDelay(delay).InsertCallback(delay, PlaySound).
        Append(spikes.DOLocalMoveY(poppedPos, popDuration).SetEase(ease).OnUpdate(CheckCollision)).
        AppendInterval(.5f).
        Append(spikes.DOLocalMoveY(hiddenPos, hideDuration).OnComplete(OnTransition));
    }

    void OnTransition() {
        if (active) {
            PlaySpikesTrap();
        }
    }

    void PlaySound() {
        if (sound != null) {
            AudioManager.Instance.Play(sound.Audio, sound.Volume, transform.position);
        }
    }

    void CheckCollision() {
        hitColliders = new Collider[5];
        Physics.OverlapBoxNonAlloc(hitPosition.position, new Vector3(2, 1, 2), hitColliders, Quaternion.identity,LayerMask.GetMask("Player", "Enemy"), QueryTriggerInteraction.Collide);

        foreach (Collider col in hitColliders) {
            if (col == null)
                break;
            
            col.GetComponent<CharacterBase>().TakeDamage(damage, false, AttackType.AttackFromFront);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(hitPosition.position, new Vector3(4, 1, 4));
    }
#endif
}