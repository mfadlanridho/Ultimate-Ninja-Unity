using UnityEngine;
using System.Collections;

public class TrapFloor : MonoBehaviour {
    [SerializeField] bool debugMode;
    
    [Space]
    [SerializeField] public FloorType Type;
    public System.Action OnThisFloorCallback;
    public bool IsOnThisFloor {get; private set;}

    private void Start() {
        if (debugMode) {
            Invoke("DebugMode", 1f);
        }
    }

    // IEnumerator ShowcaseTraps(MapStart mapStart) {
    //     yield return new WaitForSeconds(.5f);
    //     StartPlayingTraps();
    //     mapStart.OnComplete += StopPlayingTraps;
    // }

    void DebugMode() {
        StartPlayingTraps();
    }

    void StartPlayingTraps() {
        IsOnThisFloor = true;
        if (OnThisFloorCallback != null) {
            OnThisFloorCallback();
        }
    }

    void StopPlayingTraps() {
        IsOnThisFloor = false;
    }

    public void StartTraps() {
        StartPlayingTraps();
    }

    public void StopTraps() {
        StopPlayingTraps();
    }
}