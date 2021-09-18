using UnityEngine;

public class FirearmAttack : MonoBehaviour {
    [SerializeField] EnemyAIConfig config;
    int fireCount;

    EnemyStateMachine stateMachine;
    FirearmWeapon weapon;
    Animator animator;
    FireArmIK IKHandler;
    float time;

    private void Start() {
        weapon = GetComponentInChildren<FirearmWeapon>();
        stateMachine = GetComponent<EnemyStateMachine>();
        animator = GetComponent<Animator>();
        IKHandler = GetComponent<FireArmIK>();

        weapon.SetDamage(MapData.EnemyAttributesDictionary[GetComponent<Enemy>().EnemyName].Damage);
    }

    public void Fire() {
        if (stateMachine.State == EnemyState.Dead)
            return;

        IKHandler?.SetShooting(true);
        weapon.Fire();
        fireCount -= 1;

        if (fireCount <= 0) {
            IKHandler?.SetShooting(false);
        }
    }

    void InitiateFire() {    
        if (stateMachine.State == EnemyState.Dead)
            return;
        
        animator.Play("Shooting", 0, 0f);
    }

    private void Update() {
        if (stateMachine.State != EnemyState.Shooting || fireCount <= 0)
            return;

        if (time <= 0) {
            InitiateFire();
            time = Random.Range(weapon.FireRate.x, weapon.FireRate.y);
        } else {
            time -= Time.deltaTime;
        }
    }

    public void ResetFireCount() {
        fireCount = config.FireCount;
    }
}