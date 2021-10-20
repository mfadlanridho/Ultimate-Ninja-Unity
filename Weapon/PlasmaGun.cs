using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlasmaGun : Trap {
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject beam;
    [SerializeField] float duration;
    [SerializeField] float damage = 25f;

    [Space]
    [SerializeField] Sound[] sounds;
    [SerializeField] AudioSource audioSource;

    bool isPlaying;
    RaycastHit[] results;
    Vector2 rotationMovement;

    protected override void Activate() {
        base.Activate();
        PlayBeamGun();
    }

    protected override void OnEnable() {
        base.OnEnable();

        rotationMovement.x = transform.eulerAngles.y - 45f;
        rotationMovement.y = transform.eulerAngles.y + 45f;

        transform.eulerAngles = transform.eulerAngles + Vector3.up * 45f;
    }

    void PlayBeamGun() {
        SetPlaying(true);
        PlaySequence();
    }

    void StopPlayingBeamGun() {
        SetPlaying(false);
    }

    void PlaySequence() {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalRotate(Vector3.up * rotationMovement.x, duration).OnUpdate(DetectCollisionIfPlaying).SetEase(Ease.Linear));
        s.Append(transform.DOLocalRotate(Vector3.up * rotationMovement.y, duration).OnUpdate(DetectCollisionIfPlaying).SetEase(Ease.Linear));
        s.OnComplete(OnTransition);
    }

    void DetectCollisionIfPlaying() {
        if (!isPlaying)
            return;
        
        results = new RaycastHit[1];
        int hits = Physics.RaycastNonAlloc(muzzle.position, muzzle.forward, results, 20f, layerMask, QueryTriggerInteraction.Collide);
        for (int i = 0; i < hits; i++) {
            results[i].collider.GetComponent<CharacterBase>().TakeDamage(damage, false, AttackType.BigAttack, transform.forward);
        }
    }

    void SetPlaying(bool value) {
        isPlaying = value;
        beam.SetActive(value);

        if (isPlaying) {
            PlayPlayingSoundSequentially();
        } else {
            PlayEndingSound();
        }
    }

    void OnTransition() {
        if (!active) {
            StopPlayingBeamGun();
            return;
        }

        SetPlaying(!isPlaying);
        PlaySequence();
    }

    void PlayEndingSound() {
        audioSource.Stop();
        audioSource.clip = sounds[2].Audio;
        audioSource.loop = false;
        audioSource.Play();
    }

    void PlayPlayingSoundSequentially() {
        StartCoroutine(PlayPlayingSoundSequentiallyCoroutine());
    }

    IEnumerator PlayPlayingSoundSequentiallyCoroutine() {
        yield return null;

        for (int i = 0; i < 2; i++) {
            audioSource.clip = sounds[i].Audio;
            audioSource.Play();
            audioSource.loop = i == 1;

            while (audioSource.isPlaying) {
                yield return null;
            }
        }
    }
}