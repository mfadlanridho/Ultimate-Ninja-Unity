using UnityEngine;

public class CrateDetector : MonoBehaviour {
    [SerializeField] CrateMover[] crates;

    int crateCount;

    private void Start() {
        foreach (var crate in crates) {
            crate.AtTargetAction += ReduceCrateCount;
        }
        crateCount = crates.Length;
    }

    void ReduceCrateCount() {
        crateCount--;
        if (crateCount == 0) {
            MapProgression.Instance.ContinueToNextFloor();
        }
    }
}