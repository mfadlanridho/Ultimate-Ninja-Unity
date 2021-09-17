using UnityEngine;
using System.Collections.Generic;

namespace SegmentPoolingSystem {
public class BaseSpawner : MonoBehaviour {
    public Pool BasePool;

    [System.Serializable]
    public class Pool {
        [SerializeField] GameObject[] objects;
        int latestUsedIndex;

        public void Respawn(Vector3 position) {
            int toActivate = (latestUsedIndex + 1) % objects.Length;
            
            int toDeactivate = latestUsedIndex - 2;
            toDeactivate = toDeactivate < 0 ? objects.Length + toDeactivate : toDeactivate;

            objects[toDeactivate].SetActive(false);

            objects[toActivate].SetActive(true);
            objects[toActivate].transform.position = position;

            latestUsedIndex = toActivate;
        }
    }
}
}