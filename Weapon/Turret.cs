using UnityEngine;
using DG.Tweening;
using BulletPoolingSystem;

public class Turret : Trap {
    [SerializeField] float lookAtDuration;
    [SerializeField] float delayDuration;
    [SerializeField] float shootingDuration;
    
    [Space, SerializeField] Transform rail; // local z rotation
    [SerializeField] float speed = 100f;
    [SerializeField] Vector2 fireRate;

    [Space, SerializeField] Sound fireSound;
    [SerializeField] Transform muzzle;
    [SerializeField] LineRenderer laserPointer;
    [SerializeField] ParticleSystem flare;
    [SerializeField] FirearmProjectile projectile;
    [SerializeField] float damage;

    [Space]
    [SerializeField] Gradient shootingGradient;
    [SerializeField] Gradient notShootingGradient;

    Transform player;
    
    bool shooting;
    float timeToShoot;
    float timeToInterchange;

    float laserDistanceUpdateTime = .1f;

    private void Start() {
        laserPointer.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (!active) {
            if (timeToInterchange > 0) {
                SetShooting(false);
                timeToInterchange = 0;
            }
            return;
        }
        
        timeToInterchange -= Time.deltaTime;
        
        if (shooting) {
            transform.DOLookAt(player.position, lookAtDuration);
            RotateRail();
            if (timeToInterchange <= 0) {
                SetShooting(false);
            } else {
                TryShooting();
            }
        } else if (timeToInterchange <= 0) {
            SetShooting(true);
        }
    }

    public void Fire() {
        AudioManager.Instance.Play(fireSound.Audio, fireSound.Volume, muzzle.position);
        BulletPool.Instance.Spawn(muzzle.position, muzzle.rotation, damage);
        flare?.Play();
    }
    
    void RotateRail() {
        rail.Rotate(0, 0, Time.deltaTime * speed);
    }

    void SetShooting(bool value) {
        shooting = value;
        if (shooting) {
            timeToInterchange = shootingDuration;
            laserPointer.colorGradient = shootingGradient;
        } else {
            timeToInterchange = delayDuration;
            laserPointer.colorGradient = notShootingGradient;
        }
    }

    void TryShooting() {
        if (timeToShoot > 0) {
            timeToShoot -= Time.deltaTime;
        } else {
            Fire();
            timeToShoot = Random.Range(fireRate.x, fireRate.y);
        }
    }

    protected override void Activate() {
        base.Activate();
        laserPointer.gameObject.SetActive(true);
    }
}