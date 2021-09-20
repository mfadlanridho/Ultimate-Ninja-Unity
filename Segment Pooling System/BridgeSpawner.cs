using UnityEngine;

namespace SegmentPoolingSystem {
public class BridgeSpawner : MonoBehaviour {
    [SerializeField] Transform[] bridges;
    int availableIndex;

    public void Spawn(Vector3 position) {
        bridges[availableIndex].position = position;
        availableIndex = (availableIndex + 1) % bridges.Length;
    }
}
}