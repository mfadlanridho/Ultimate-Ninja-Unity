using UnityEngine;
using DG.Tweening;

public class DashAttack : AttackBase {
    [SerializeField] string attackName;
    [SerializeField] float offset = 1f;
    [SerializeField] float duration = 1f;
    EnemyTarget target;

    protected override void Start() {
        base.Start();
        target = FindObjectOfType<EnemyTarget>();
    }

    public void InitiateAttack() {
        if ( PlayerStateMachine.Instance.State == PlayerState.None ) {
            if (target.CurrentTarget) {
                Attack(attackName, 1f);
            }
        }
    }

    // animator event
    public void OnDash() {
        Vector3 t = target.CurrentTarget.position;
        t.y = transform.position.y;
        transform.DOMove(t - transform.forward * offset, duration);
    }
}