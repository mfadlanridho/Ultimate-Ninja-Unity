using UnityEngine;
using System.Collections.Generic;

/*
    Game manager is for the Gameplay portion only
*/

namespace SegmentPoolingSystem {
public class GameManager : MonoBehaviour {
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }
    #endregion

    public int CurrentSegment;
    public int Increment = 20;
    public int TotalSegmentCount = 10;
    public System.Action MoveToNextSegmentEvent;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
    }

    public void MoveToNextSegment() {
        CurrentSegment++;

        if (MoveToNextSegmentEvent != null) {
            MoveToNextSegmentEvent();
        }
    }

    public List<List<TrapSpawner.Type>> GetTrapTypesToSpawn() {
        // the trap types configurations
        // refer to https://docs.google.com/spreadsheets/d/1AToHR9ekVvHxYvJ0AKbyFjw-T2rnY0dK5U8jBnisfTs/edit?usp=sharing

        TrapSpawner.Type[] trapTypes = new TrapSpawner.Type[] {TrapSpawner.Type.Spikes, TrapSpawner.Type.Lasers, TrapSpawner.Type.Turrets, TrapSpawner.Type.Flames};
        
        List<List<TrapSpawner.Type>> toSpawn = new List<List<TrapSpawner.Type>>() {
            new List<TrapSpawner.Type>(){TrapSpawner.Type.Spikes},
            new List<TrapSpawner.Type>(){TrapSpawner.Type.Lasers},
            new List<TrapSpawner.Type>(){TrapSpawner.Type.Turrets},
            new List<TrapSpawner.Type>(){TrapSpawner.Type.Flames},
        };

        if (GameConfiguration.LevelIndex >= 4) {
            foreach (var item in Utilities.GetKCombsWithRept(trapTypes, 2)) {
                toSpawn.Add(new List<TrapSpawner.Type>(item));
            }
        }

        if (GameConfiguration.LevelIndex >= 16) {
            foreach (var item in Utilities.GetKCombsWithRept(trapTypes, 3)) {
                toSpawn.Add(new List<TrapSpawner.Type>(item));
            }        
        }

		toSpawn.RemoveRange(GameConfiguration.LevelIndex + 1, toSpawn.Count - GameConfiguration.LevelIndex - 1);
        return toSpawn;
    }

    public int GetFloorCountToBeSpawned() {
        return 2 * Mathf.FloorToInt(GameConfiguration.LevelIndex/ 5 ) + 3;
    }
}
}