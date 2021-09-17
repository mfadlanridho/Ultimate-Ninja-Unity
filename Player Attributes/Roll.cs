using UnityEngine;

public class Roll : AbilityWithCooldown {
    PlayerStateMachine stateMachine;
    Animator animator;
    float speed = 10;
    public bool Rolling;

    Collider col;

    private void Start() {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update() {
        if (Rolling) {
            if (PlayerStateMachine.Instance.Colliding)
                return;
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
        }
    }

    public void DoRoll() {
        if (stateMachine.State == PlayerState.Dead || stateMachine.State == PlayerState.Victory)
            return;
        
        animator.Play("Roll");
    }

    // animator event
    void RollingStart() {
        Rolling = true;
        col.enabled = false;
    }

    public void RollingStop() {
        Rolling = false;
        col.enabled = true;
    }
}