using UnityEngine;

namespace SegmentPoolingSystem {
public class BaseSpawner : MonoBehaviour {
    public Pool BasePool;

    [System.Serializable]
    public class Pool {
        [SerializeField] GameObject[] objects;
        int latestUsedIndex;

        public void ActivateNext(Vector3 position) {
            int toActivate = (latestUsedIndex + 1) % objects.Length;
            
            objects[toActivate].SetActive(true);
            objects[toActivate].transform.position = position;

            latestUsedIndex = toActivate;
        }

        public void DeactivatePrevious() {
            int toDeactivate = latestUsedIndex - 1;
            toDeactivate = toDeactivate < 0 ? objects.Length + toDeactivate : toDeactivate;

            objects[toDeactivate].SetActive(false);
        }
    }
}
}