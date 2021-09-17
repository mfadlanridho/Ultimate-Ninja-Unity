using UnityEngine;

public class EnemyStateMachine : MonoBehaviour{
    public EnemyState State {
        get {
            return state;
        }
        set {
            if (state != EnemyState.Dead) {
                state = value;
            }
        }
    }
    private EnemyState state;

    void ResetState() {
        state = EnemyState.Moving;
    }

    private void OnEnable() {
        ResetState();
    }
}

public enum EnemyState {
    Moving,
    InRange,
    Hit,
    Attacking,
    Dead,
    Shooting,
    Stopped
}