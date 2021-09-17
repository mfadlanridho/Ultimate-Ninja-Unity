using UnityEngine;

public class Enemy : CharacterBase {
    [SerializeField] public EnemyDetector enemyDetector;
    [SerializeField] public EnemyDeath enemyDeath;

    public EnemyName EnemyName;
    public EnemyRange Range;
    public System.Action<Enemy> OnEnemyDeath;
    public System.Action OnSpawn;
    [HideInInspector] public bool InGame;
    
    Collider col;

    protected override void Die() {
        EnemyStateMachine stateMachine = GetComponent<EnemyStateMachine>();
        if (stateMachine != null) {
            stateMachine.State = EnemyState.Dead;
        }
        GetComponent<Animator>().Play("Death");

        enemyDetector.RemoveEnemy(this);
        enemyDeath.DeactivateEnemy(this);

        col.isTrigger = true;
        base.Die();
    }

    public void GetInGame(Vector3 position) {
        if (OnSpawn != null) {
            OnSpawn();
        }
        SetMaxHealth(MapData.EnemyAttributesDictionary[EnemyName].MaxHealth);

        transform.position = position;
        
        InGame = true;

        if (col == null) {
            col = GetComponent<Collider>();
        }
        
        col.isTrigger = false;
        enemyDetector.AddEnemy(this);
        gameObject.SetActive(true);

        GetComponent<BurnHandler>().StopBurn();
    }
}

[System.Serializable]
public enum EnemyRange {
    Melee,
    Ranged,
    NonAI
}