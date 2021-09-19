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

    public int Increment {get {return 30;}}

    public int CurrentSegment {get; private set;}
    public int StarsPickedUp {get; private set;}
    public int TotalStars {get; private set;}

    public System.Action MoveToNextSegmentEvent;
    public System.Action FinalSegmentEvent;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
    }

    public void SetTotalStars(int value) {
        TotalStars = value; 
    }

    public void PickUpStar() {
        StarsPickedUp++;
    }

    public void ContinueSegment() {
        if (CurrentSegment == FloorCountToBeSpawned) {
            Debug.Log("In Final Segment");
            if (FinalSegmentEvent != null) {
                FinalSegmentEvent();
            }
            return;
        }

        CurrentSegment++;
        if (MoveToNextSegmentEvent != null) {
            MoveToNextSegmentEvent();
        }
    }

    int floorCountToBeSpawned;
    public int FloorCountToBeSpawned {
        get {
            if (floorCountToBeSpawned == 0)
                floorCountToBeSpawned =  2 * Mathf.FloorToInt(GameConfiguration.LevelIndex/ 5 ) + 3;
            return floorCountToBeSpawned;
        }
    }
}
}