using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    EnemyStateMachine stateMachine;
    AIMoveToFormation ai;
    bool targetOverride;

    public bool Colliding;

    private void Start() {
        stateMachine = GetComponent<EnemyStateMachine>();
        ai = GetComponent<AIMoveToFormation>();
    }

    Collider[] hitColliders = new Collider[1];
    public bool DetectCollision(Vector3 position, string additionalMask = null) {
        int mask = additionalMask == null ? LayerMask.GetMask("Edge", "Props") 
            : LayerMask.GetMask("Edge", "Props", additionalMask);
        
        hitColliders[0] = null;
        Physics.OverlapSphereNonAlloc(position, .5f, hitColliders, mask);

        if (hitColliders[0] != null) {
            return true;
        }
        return false;
    }

}