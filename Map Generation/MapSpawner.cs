using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class MapSpawner : MonoBehaviour {
    [SerializeField] FloorTypeObjectDictionary floorTypeObjectDictionary;
    public IDictionary<FloorType, FloorObjects> FloorTypeObjectDictionary {
		get { return floorTypeObjectDictionary; }
		set { floorTypeObjectDictionary.CopyFrom (value); }
	}
    [SerializeField] GameObject finalFloor;

    [Space]
    [SerializeField] GameObject[] roads;
    [SerializeField] GameObject[] sidewalks;

    [Space]
    [SerializeField] GameObject star;
    [SerializeField] GameObject wall;

    [Space, SerializeField] int floorsToSpawn;

    [Header("For stars")]
    [SerializeField] TextMeshProUGUI starTextUI;

    List<GameObject> spawnedFloors;
    
    [Header("Minimum 4")]
    [SerializeField] Pool roadPool;
    [SerializeField] Pool sidewalkPool;

    private void Start() {
        SpawnMapConfigurationFloors();
        MapProgression.Instance.OnMovingToNextFloor += OnMovingToNextFloor;

        // FindObjectOfType<EdgeMove>().OnArrivedInPosition += OnArrivedInPosition;
        // FindObjectOfType<MapStart>().OnComplete += DeactivateAllOtherFloors;
        DeactivateAllOtherFloors();
    }

    void OnMovingToNextFloor() {
        spawnedFloors[MapProgression.Instance.CurrentFloor - 1].GetComponent<TrapFloor>().StopTraps();

        if (MapProgression.Instance.CurrentFloor + 1 < spawnedFloors.Count) {
            spawnedFloors[MapProgression.Instance.CurrentFloor + 1].SetActive(true);
        }
    }

    void OnArrivedInPosition() {
        if (MapProgression.Instance.CurrentFloor - 2 >= 0) {
            spawnedFloors[MapProgression.Instance.CurrentFloor - 2].SetActive(false);
        }
        spawnedFloors[MapProgression.Instance.CurrentFloor].GetComponent<TrapFloor>().StartTraps();
    }

    void ActivateTrap() {
        var curFloor = spawnedFloors[MapProgression.Instance.CurrentFloor];
        curFloor.SetActive(false);
        curFloor.GetComponent<TrapFloor>().StartTraps();
    }

    void DeactivateAllOtherFloors() {
        for (int i = 3; i < spawnedFloors.Count; i++) {
            spawnedFloors[i].SetActive(false);
        }
    }

    Transform SpawnFloor(FloorType typeToSpawn, Vector3 position, int index = -1) {
        TrapFloor trapFloor = SpawnTrapFloor(typeToSpawn, position);
        spawnedFloors.Add(trapFloor.gameObject);

        Transform road = FloorTypeObjectDictionary[typeToSpawn].OverrideRoad ? new GameObject("road").transform : Instantiate(roads[Random.Range(0, roads.Length)], position, Quaternion.identity, trapFloor.transform).transform;
        Transform sidewalk = Instantiate(sidewalks[Random.Range(0, sidewalks.Length)], position, Quaternion.identity,trapFloor.transform).transform;
        Transform walll = Instantiate(wall, position, Quaternion.identity,trapFloor.transform).transform;

        return trapFloor.transform;
    }

    void SpawnFinalFloor() {
        Vector3 position = Vector3.right * MapAttributes.Instance.FloorCountToBeSpawned * MapAttributes.Instance.XIncrement;
        Transform finalFloorRoadNSidewalk = Instantiate(finalFloor, position, Quaternion.identity).transform;
    }

    TrapFloor SpawnTrapFloor(FloorType type, Vector3 position) {
        GameObject toSpawn = FloorTypeObjectDictionary[type].Objects[Random.Range(0, FloorTypeObjectDictionary[type].Objects.Count)];
        Transform trapFloor = Instantiate(toSpawn, position, Quaternion.identity).transform;
        return trapFloor.GetComponent<TrapFloor>();
    }

    void SpawnMapConfigurationFloors() { 
        #region delete the spawned from editor if exist
        Transform spawnedParent = transform.Find("Generated Map");
        if (spawnedParent != null)
            DestroyImmediate(spawnedParent.gameObject);
        #endregion
        
        spawnedFloors = new List<GameObject>();
        
        int availableFloorCount = MapAttributes.Instance.SpawnableFloorTypeCount;
        
        int starSpawnIndex = 1;
        int starSpawned = 0;
        int starSpawnDiff = Mathf.Max(Mathf.FloorToInt(MapAttributes.Instance.FloorCountToBeSpawned / 3), 3);
        
        for (int i = 0; i < MapAttributes.Instance.FloorCountToBeSpawned; i++) {
            Vector3 spawnPosition = Vector3.right * i * MapAttributes.Instance.XIncrement;
            
            FloorType typeToSpawn;
            if (i == 0)
                typeToSpawn = FloorType.Default;
            else
                typeToSpawn = MapAttributes.Instance.AvailableFloorConfiguration.FloorTypes[Mathf.Abs(availableFloorCount-i) % availableFloorCount ];

            var objects = floorTypeObjectDictionary[typeToSpawn].Objects;
            int randomNumber = Random.Range(0, objects.Count);
            Transform map = SpawnFloor(typeToSpawn, spawnPosition, i);

            if (MapAttributes.Instance.LatestMap && i == starSpawnIndex && starSpawned < 3) {
                SpawnStars(spawnPosition, map);
                starSpawnIndex += starSpawnDiff;
                starSpawned++;
            }
        }
        SpawnFinalFloor();
    }

#region editor functions
#if UNITY_EDITOR
    public void GenerateMap() {
        spawnedFloors = new List<GameObject>();
        string parentName = "Generated Map";

        Transform spawnedParent = transform.Find(parentName);
        if (spawnedParent != null)
            DestroyImmediate(spawnedParent.gameObject);
        
        spawnedParent = new GameObject(parentName).transform;
        spawnedParent.parent = transform;

        for (int i = 0; i < floorsToSpawn; i++) {
            FloorType typeToSpawn = (FloorType)Random.Range(1, FloorTypeObjectDictionary.Count);
            SpawnFloor(typeToSpawn ,Vector3.right * 20f * i);
        }
    }

    public void FindTrapFloorsInResources() {
        FloorTypeObjectDictionary.Clear();
        TrapFloor[] trapFloors = Resources.LoadAll("1. Traps", typeof(TrapFloor)).Cast<TrapFloor>().ToArray();

        for (int i = 0; i < trapFloors.Length; i++) {
            TrapFloor trapFloor = trapFloors[i];
            if(FloorTypeObjectDictionary.ContainsKey(trapFloor.Type)) {
                FloorTypeObjectDictionary[trapFloor.Type].Objects.Add(trapFloor.gameObject);
            } else {
                FloorObjects floorObjects = new FloorObjects();
                floorObjects.Objects = new List<GameObject>();
                floorObjects.Objects.Add(trapFloor.gameObject);

                FloorTypeObjectDictionary.Add(trapFloor.Type, floorObjects);
            }
        }
    }
#endif
#endregion
#region star spawn
    void SpawnStars(Vector3 position, Transform parent) {
        Transform instantiatedStar = Instantiate(star, position + Vector3.up * 3.5f, Quaternion.identity, parent).transform;
        instantiatedStar.GetComponent<StarPickUp>().SetTextUI(starTextUI);

        MapProgression.Instance.SetTotalStars(MapProgression.Instance.TotalStars + 10);
    }

    void SpawnTenStars(Vector3 position, Transform parent) {
        int[] xPositions = new int[] { -10, -5, 0 , 5, 10};

        for (int i = 0; i < 5; i++) {
            // upper side
            Vector3 starSpawnPos = position + Vector3.right * xPositions[i] + Vector3.forward * 3.7f;
            SpawnStars(starSpawnPos, parent);

            // lower side
            starSpawnPos = position + Vector3.right * xPositions[i] + Vector3.forward * -3.7f;
            SpawnStars(starSpawnPos, parent);
        }
    }
#endregion
}

[System.Serializable]
public class FloorObjects {
    [SerializeField] public bool OverrideRoad;
    [SerializeField] public List<GameObject> Objects;
}

[System.Serializable]
public class Pool {
    [SerializeField] GameObject[] Objects;
    [SerializeField] HashSet<int> UsedObjectIndex;
}