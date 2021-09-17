using UnityEngine;

public class FireArmIK : MonoBehaviour {
    [SerializeField] Transform muzzle;
    [SerializeField] Transform bone;
    [SerializeField] Vector3 offset;

    [Header("Stop IK")]
    [SerializeField] float minDistance = 1.5f;
    [SerializeField, Range(0, 1)] float weight = 1f;

    [Space]
    [SerializeField] float iteration = 5f;
    [SerializeField] float inaccuracy = .4f;

    Transform player;
    EnemyStateMachine stateMachine;

    bool shooting;

    private void Start() {
        Enemy enemy = GetComponent<Enemy>();
        enemy.OnDeath += DisableScript;
        enemy.OnSpawn += EnableScript;
        player = GameObject.FindWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    void DisableScript() {
        this.enabled = false;
    }

    void EnableScript() {
        this.enabled = true;
    }

    void LateUpdate() {
        if (!(stateMachine.State == EnemyState.InRange || stateMachine.State == EnemyState.Shooting))
            return;
        
        for (int i = 0; i < iteration; i++) {
            AimAtPlayer();   
        }
    }

    Vector3? GetIKDirection() {
        Vector3 targetDirection = (player.position + offset) - muzzle.position;
        Vector3 aimDirection = muzzle.forward;
        float blendOut = .0f;

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < minDistance) {
            blendOut += minDistance - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return direction;
    }

    void AimAtPlayer() {
        Vector3? targetDirection = GetIKDirection();
        if (targetDirection != null) {
            if (shooting) {
                targetDirection += Random.insideUnitSphere * inaccuracy;
            }
            
            Quaternion aimTowards = Quaternion.FromToRotation(muzzle.forward, (Vector3)targetDirection);
            Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
            bone.rotation =  blendedRotation * bone.rotation;
        }
    }

    public void SetShooting(bool value) {
        shooting = value;
    }
}