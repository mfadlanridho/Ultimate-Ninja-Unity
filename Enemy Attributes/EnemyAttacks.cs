using UnityEngine;

public class EnemyAttacks : MonoBehaviour {
    [SerializeField] int attacksCount = 1;
    int curAtkIndex;
    bool attacking;
    bool isMelee;

    private void Start() {
        isMelee = GetComponent<Enemy>().Range == EnemyRange.Melee;
    }

    public void Attack(Animator animator) {
        if (isMelee && attacking)
            return;
        
        attacking = true;
        animator.SetTrigger("Attack");
        curAtkIndex = (curAtkIndex + 1) % attacksCount;
        animator.SetTrigger(curAtkIndex.ToString());
    }

    public void ResetTriggers(Animator animator) {
        attacking = false;
        animator.ResetTrigger("Attack"); // make sure this fucked up stupid trigger isnt keep being ticked
    }
}