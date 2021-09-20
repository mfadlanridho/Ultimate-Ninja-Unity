using UnityEngine;
using System.Collections.Generic;

namespace SegmentPoolingSystem {
public class SegmentSpawner : MonoBehaviour {
    #region positions array
    Vector3[][] sidesPositionsArray = new Vector3[][] {
        new Vector3[] {new Vector3(0, 0, 7.5f)},
        new Vector3[] {new Vector3(-7.5f, 0, -7.5f), new Vector3(7.5f, 0, 7.5f)},
        new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0)},
    };

    Vector3[][] spikesPositionsArray = new Vector3[][] {
        new Vector3[] {new Vector3(2.5f, 0, 2.5f), new Vector3(-2.5f, 0, -2.5f)},
        new Vector3[] {new Vector3(-7.5f, 0, 2.5f), new Vector3(7.5f, 0, -2.5f)},
        new Vector3[] {new Vector3(0, 0, 0)}
    };
    #endregion

    [SerializeField] Transform finalFloor;

    TrapSpawner trapSpawner;
    BaseSpawner baseSpawner;
    BridgeSpawner bridgeSpawner;

    List<TrapSpawner.Type>[] trapArray;
    int latestIndex;
    int activeSegmentCount = 2;

    private void Awake() {
        trapArray = new List<TrapSpawner.Type>[activeSegmentCount];
        for (int i = 0; i < trapArray.Length; i++) {
            trapArray[i] = new List<TrapSpawner.Type>();
        }

        // the trap types configurations
        // refer to https://docs.google.com/spreadsheets/d/1AToHR9ekVvHxYvJ0AKbyFjw-T2rnY0dK5U8jBnisfTs/edit?usp=sharing

        TrapSpawner.Type[] trapTypes = new TrapSpawner.Type[] {TrapSpawner.Type.Spikes, TrapSpawner.Type.Lasers, TrapSpawner.Type.Turrets, TrapSpawner.Type.Flames};
        
        toSpawn = new List<List<TrapSpawner.Type>>() {
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

        if (GameConfiguration.LevelIndex + 1 < toSpawn.Count)
            toSpawn.RemoveRange(GameConfiguration.LevelIndex + 1, toSpawn.Count - GameConfiguration.LevelIndex - 1);
    }

    List<List<TrapSpawner.Type>> toSpawn;
    private void Start() {
        trapSpawner = GetComponent<TrapSpawner>();
        baseSpawner = GetComponent<BaseSpawner>();
        bridgeSpawner = GetComponent<BridgeSpawner>();

        FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent += ArrivedInSegmentCallback;
        
        GameManager.Instance.MoveToNextSegmentEvent += ActivateNextSegment;
        Spawn(Vector3.zero);
    }

    void ArrivedInSegmentCallback() {
        DisablePreviousSegment();

        if (GameManager.Instance.CurrentSegment != GameManager.Instance.FloorCountToBeSpawned)
            bridgeSpawner.Spawn(Vector3.right * (GameManager.Instance.Increment * GameManager.Instance.CurrentSegment + 15f));
    }

    void DisablePreviousSegment() {
        baseSpawner.DeactivatePrevious();

        int toDeactivate = latestIndex - 1;
        toDeactivate = toDeactivate < 0 ? activeSegmentCount + toDeactivate : toDeactivate;

        foreach (var item in trapArray[toDeactivate]) {
            trapSpawner.TrapPoolDictionary[item].StoreToPool();
        }
        trapArray[toDeactivate].Clear();
    }

    void ActivateNextSegment() {
        int segmentToSpawn = GameManager.Instance.CurrentSegment;
        if (segmentToSpawn >= GameManager.Instance.FloorCountToBeSpawned) {
            Debug.Log("Respawn stopped, exceeding floor count to be spawned");
            finalFloor.position = Vector3.right * segmentToSpawn * GameManager.Instance.Increment;
            finalFloor.gameObject.SetActive(true);
            FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent -= ActivateNextSegment;
            return;
        }
        
        int toSpawnIndex = toSpawn.Count - 1 - GameManager.Instance.CurrentSegment;

        while (toSpawnIndex < 0)
            toSpawnIndex = toSpawn.Count + toSpawnIndex;
        Debug.Log(toSpawnIndex);

        Spawn(Vector3.right * segmentToSpawn * GameManager.Instance.Increment, toSpawn[toSpawnIndex], segmentToSpawn);
    }

    void Spawn(Vector3 position, List<TrapSpawner.Type> types = null, int segmentIndex = 0) {
        baseSpawner.ActivateNext(position);

        int toActivate = (latestIndex + 1) % activeSegmentCount;
        if (types != null) {
            int sidesCount = 0;
            int spikesCount = 0;
            foreach (var item in types) {
                if (item == TrapSpawner.Type.Spikes) {
                    foreach (var item2 in spikesPositionsArray[spikesCount]) {
                        trapSpawner.TrapPoolDictionary[item].Spawn(position + item2, segmentIndex);
                        trapArray[toActivate].Add(item);
                    }
                    spikesCount++;
                } else {
                    trapSpawner.TrapPoolDictionary[item].Spawn(position + sidesPositionsArray[types.Count - 1 - spikesCount][sidesCount], segmentIndex);
                    trapArray[toActivate].Add(item);
                    sidesCount++;
                }
            }
        }

        latestIndex = toActivate;
    }
}
}