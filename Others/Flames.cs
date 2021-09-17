using UnityEngine;
using System.Collections;

public class Flames : Trap {
    [SerializeField] Sound sound;
    [SerializeField] ParticleSystem ps;
    [SerializeField] Transform hitPosition;
    [SerializeField] float damage;

    float time;
    bool emitting;

    Collider[] hitColliders = new Collider[5];

    protected override void Activate() {
        base.Activate();
        PlayFlames();
    }

    void PlayFlames() {
        StartCoroutine(RepeatFlames());
    }
        
    void Update() {
        if (emitting && time > 0) {
            time -= Time.deltaTime;
            CheckCollision();
        }
        else if (emitting) {
            emitting = false;
        }
    }

    IEnumerator RepeatFlames() {
        while (active) {
            Inititate();
            yield return new WaitForSeconds(5f);
        }
        yield return null;
    }

    void Inititate() {
        if (sound != null)
            AudioManager.Instance.Play(sound.Audio, sound.Volume, transform.position);
            
        ps.Play();
        time = ps.main.duration;
        emitting = true;
    }

    void CheckCollision() {        
        hitColliders = new Collider[5];
        Physics.OverlapBoxNonAlloc(hitPosition.position, new Vector3(1, 1, 10), hitColliders, Quaternion.identity, LayerMask.GetMask("Player", "Enemy"), QueryTriggerInteraction.Collide);

        foreach (Collider col in hitColliders) {
            if (col == null)
                return;
            
            CharacterBase character = col.GetComponent<CharacterBase>();
            character?.TakeDamage(damage, false, AttackType.BigAttack);

            BurnHandler burnHandler = col.GetComponent<BurnHandler>();
            burnHandler?.Ignite(character);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (hitPosition != null)
            Gizmos.DrawWireCube(hitPosition.position, new Vector3(2, 2, 20));
    }
    #endif
}