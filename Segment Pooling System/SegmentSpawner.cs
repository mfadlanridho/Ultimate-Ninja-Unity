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

    List<TrapSpawner.Type>[] trapArray;
    int latestIndex;
    int activeSegmentCount = 4;
    int floorCountToBeSpawned;

    private void Awake() {
        trapArray = new List<TrapSpawner.Type>[activeSegmentCount];
        for (int i = 0; i < trapArray.Length; i++) {
            trapArray[i] = new List<TrapSpawner.Type>();
        }
    }

    List<List<TrapSpawner.Type>> toSpawn;
    private void Start() {
        floorCountToBeSpawned = GameManager.Instance.GetFloorCountToBeSpawned();

        FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent += ArrivedInSegmentCallback;
        trapSpawner = GetComponent<TrapSpawner>();
        baseSpawner = GetComponent<BaseSpawner>();

        toSpawn = GameManager.Instance.GetTrapTypesToSpawn();
        Respawn(Vector3.zero);
        Respawn(Vector3.right * GameManager.Instance.Increment, toSpawn[toSpawn.Count - 1], 1);
    }

    void ArrivedInSegmentCallback() {
        int segmentToSpawn = GameManager.Instance.CurrentSegment + 1;
        if (segmentToSpawn >= floorCountToBeSpawned) {
            Debug.Log("Respawn stopped, exceeding floor count to be spawned");
            finalFloor.position = Vector3.right * segmentToSpawn * GameManager.Instance.Increment;
            finalFloor.gameObject.SetActive(true);
            FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent -= ArrivedInSegmentCallback;
            return;
        }

        int toSpawnIndex = toSpawn.Count - 1 - GameManager.Instance.CurrentSegment;
        toSpawnIndex = toSpawnIndex < 0 ? toSpawn.Count + toSpawnIndex : toSpawnIndex;
        Respawn(Vector3.right * segmentToSpawn * GameManager.Instance.Increment, toSpawn[toSpawnIndex], segmentToSpawn);
    }

    void Respawn(Vector3 position, List<TrapSpawner.Type> types = null, int segmentIndex = 0) {
        baseSpawner.BasePool.Respawn(position);

        if (types == null)
            return;

        // log the types to spawn
        if (false) {
            #if UNITY_EDITOR
            // string str = "";
            // foreach (var item in types) {
            //     str += item.ToString();
            // }
            // Debug.Log("Spawning " + str);
            #endif
        }

        int toDeactivate = latestIndex - 2;
        toDeactivate = toDeactivate < 0 ? activeSegmentCount + toDeactivate : toDeactivate;

        foreach (var item in trapArray[toDeactivate]) {
            trapSpawner.TrapPoolDictionary[item].StoreToPool();
        }
        trapArray[toDeactivate].Clear();

        int sidesCount = 0;
        int spikesCount = 0;

        int toActivate = (latestIndex + 1) % activeSegmentCount;
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

        latestIndex = toActivate;
    }
}
}