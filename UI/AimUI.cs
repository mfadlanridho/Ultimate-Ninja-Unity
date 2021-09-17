using UnityEngine;

public class AimUI : MonoBehaviour {
    [SerializeField] Vector3 offset;

    SpriteRenderer spriteRenderer;
    Transform cam;

    EnemyTarget enemyTarget;

    private void Start() {
        cam = Camera.main.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        enemyTarget = FindObjectOfType<EnemyTarget>();

        EnemyDetector detector = FindObjectOfType<EnemyDetector>();
        DeactivateUI();
        detector.OnFoundEnemy += ActivateUI;
        detector.OnNoEnemy += DeactivateUI;
    }
    
    private void LateUpdate() {
        if (enemyTarget.CurrentTarget != null) {
            transform.position = enemyTarget.CurrentTarget.position + offset;
            transform.forward = -cam.forward;
        }
    }

    void ActivateUI () {
        Color tmp = spriteRenderer.color;
        tmp.a = 1f;
        spriteRenderer.color = tmp;
    }

    void DeactivateUI () {
        Color tmp = spriteRenderer.color;
        tmp.a = 0f;
        spriteRenderer.color = tmp;
    }
}