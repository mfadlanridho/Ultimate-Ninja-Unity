using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {
    # region Singleton
    public static PlayerStateMachine Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    # endregion

    [HideInInspector] public PlayerState State {get; private set;}

    public bool SetState(PlayerState state) {
        if (state == PlayerState.Reviving) {
            State = state;
            return true;
        }
        else if (State == PlayerState.Dead || PlayerStats.Instance.GameState == GameState.InMenu) {
            return false;
        }
        
        State = state;
        return true;
    }

    public void ResetState() {
        State = PlayerState.None;
    }

    [HideInInspector] public bool Colliding;
}

public enum PlayerState {
    None,
    Attacking,
    Rolling,
    Dead,
    InMenu,
    Victory,
    Reviving
}