using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    [SerializeField] float attackableDist = 1.5f;
    [SerializeField] float angle = 15f;

    public System.Action OnAttack;
    float damage;
    int attackLayer;

    private void Start() {
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null) {
            attackLayer = LayerMask.GetMask("Player");
            damage = MapData.EnemyAttributesDictionary[enemy.EnemyName].Damage;
            
        } else {
            attackLayer = LayerMask.GetMask("Enemy");
            damage = PlayerStats.Instance? PlayerStats.Instance.Damage : 0;
        }
    }


    public void Attack(AttackType type) {
        if (OnAttack != null) 
            OnAttack();
                
        Collider[] hits = Physics.OverlapSphere(transform.position, attackableDist, attackLayer, QueryTriggerInteraction.Collide);

        foreach (Collider hit in hits) {
            if (hit.transform == transform)
                continue;

            if (type == AttackType.SpinningSlash) {
                hit.GetComponent<CharacterBase>()?.TakeDamage(damage, false, AttackType.BigAttack, transform.forward);
                continue;
            }
            
            if (IsInSight(hit.transform)) {
                hit.GetComponent<CharacterBase>()?.TakeDamage(damage, false, type, transform.forward);
            }
        }
    }

    bool IsInSight(Transform target) {
        Vector3 path = target.position - transform.position;
        
        path.y = 0f;
        float deltaAngle = Vector3.Angle(path, transform.forward);
        if (deltaAngle > angle) {
            return false;
        }
        return true;
    }

}