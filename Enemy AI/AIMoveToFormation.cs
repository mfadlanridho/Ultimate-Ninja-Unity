using UnityEngine;

public class AIMoveToFormation : MonoBehaviour {
    [SerializeField] public EnemyAIConfig Config;

    EnemyStateMachine stateMachine;
    CollisionDetection collisionDetection;
    Transform player;
    Animator animator;
    Enemy enemy;

    Vector3 formationTargetPosition;
    Vector3 assignedTargetPosition;

    public Vector3 FormationTargetPosition => formationTargetPosition;
    public Vector3 Path {
        get {
            return path;
        }
    }

    Vector3 path;
    Vector3 localPath;
    bool movingBackwards;

    bool assignedTarget;
    [HideInInspector] 
    public int OccupiedPositionIndex = -1;
    float speed;

#region unity methods
    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
        collisionDetection = GetComponent<CollisionDetection>();
        enemy = GetComponent<Enemy>();

        EnemyName enemyName = GetComponent<Enemy>().EnemyName;
        speed = MapData.EnemyAttributesDictionary[enemyName].Speed;
    }

    void Update() {
        if (stateMachine.State != EnemyState.Moving || !assignedTarget)
            return;

        path = GetFormationPath();
        localPath = GetLocalPath(path);

        movingBackwards = localPath.z < 0;
        collisionDetection.Colliding = collisionDetection.DetectCollision(transform.position + path.normalized, movingBackwards ? "Boundaries" : null);

        if (collisionDetection.Colliding) {
            if (movingBackwards) {
                if (EdgedByPlayer()) {
                    animator.SetBool("InRange", true);
                }
                return;
            }
            CheckOtherDirections();
        } else {
            assignedTargetPosition = formationTargetPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, assignedTargetPosition, speed * Time.deltaTime);

        if (ClosingInToTargetPosition(path)) { 
            animator.SetBool("InRange", true);
            return;
        }

        animator.SetFloat("Horizontal", localPath.x);
        animator.SetFloat("Vertical", localPath.z);
    }

    void CheckOtherDirections() {
        for (int angle = 30; angle < 180; angle += 30) {
            Vector3 normalizedPath = path.normalized;
            Vector3 dir = transform.position + Quaternion.Euler(0, angle, 0) * normalizedPath;

            bool collided = collisionDetection.DetectCollision(dir, movingBackwards ? "Boundaries" : null);
            if (!collided) {
                assignedTargetPosition = dir;
                return;
            }

            dir = transform.position + Quaternion.Euler(0, -1 * angle, 0) * normalizedPath;
            collided = collisionDetection.DetectCollision(dir, movingBackwards ? "Boundaries" : null);
            if (!collided) {
                assignedTargetPosition = dir;
                return;
            }
        }
    }

    void OnDrawGizmos() {
        Vector3 from = transform.position + Vector3.up;
        Vector3 to = from + GetFormationPath();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }
#endregion

#region public methods
    public void SetTargetPosition(Vector3 position) {
        formationTargetPosition = position;
        assignedTarget = true;
    }
#endregion

    Vector3 GetFormationPath() {
        Vector3 path = formationTargetPosition - transform.position;
        path.y = 0f;
        return path;
    }

    bool ClosingInToTargetPosition(Vector3 path) {
        return Vector3.SqrMagnitude(path) < .1f;
    }

    Vector3 GetLocalPath(Vector3 path) {
        return transform.InverseTransformDirection(path);
    }

    bool NearTarget() {
        return (player.position - transform.position).sqrMagnitude <= Mathf.Pow(Config.WaitDist, 2f);
    }

    public bool EdgedByPlayer() {
        return collisionDetection.Colliding && NearTarget();
    }
}