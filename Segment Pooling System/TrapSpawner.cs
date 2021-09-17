using UnityEngine;
using System.Collections.Generic;

namespace SegmentPoolingSystem {
public class TrapSpawner : MonoBehaviour {
    public TrapPoolDictionary TrapPoolDictionary;

    [System.Serializable]
    public class Pool {
        [SerializeField] Trap prefab;

        List<Trap> objects = new List<Trap>();
        Queue<Trap> usedObjects = new Queue<Trap>();

        public void Spawn(Vector3 position, int segmentIndex) {
            Trap trapToSpawn = null;
            Vector3 rotation = position.z > 0 ? Vector3.up * 180f : Vector3.zero;
                
            if (usedObjects.Count == objects.Count) {
                trapToSpawn = Instantiate(prefab.gameObject, position, Quaternion.Euler(rotation)).GetComponent<Trap>();
                objects.Add(trapToSpawn);
            } else {
                foreach (var item in objects) {
                    if (!usedObjects.Contains(item)) {
                        trapToSpawn = item;
                        trapToSpawn.gameObject.SetActive(true);
                        trapToSpawn.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
                    }
                }
            }
            trapToSpawn.SetSegmentIndex(segmentIndex);
            usedObjects.Enqueue(trapToSpawn);
        }

        public void StoreToPool() {
            usedObjects.Dequeue().gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public enum Type {
        Spikes,
        Lasers,
        Turrets,
        Flames
    }
}
}