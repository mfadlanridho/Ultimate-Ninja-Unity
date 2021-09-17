using UnityEngine;
using SegmentPoolingSystem;

public class Trap : MonoBehaviour {
    SegmentMoveHandler moveHandler;
    int segmentIndex;
    protected bool active {get; private set;}

    private void OnEnable() {
        if (moveHandler == null) {
            moveHandler = FindObjectOfType<SegmentMoveHandler>();
        }
        moveHandler.ArrivedInSegmentEvent += TryActivate;
    }

    public void SetSegmentIndex(int segmentIndex) {
        this.segmentIndex = segmentIndex;
    }
    
    void TryActivate() {
        if (GameManager.Instance.CurrentSegment == segmentIndex) {
            Activate();
            GameManager.Instance.MoveToNextSegmentEvent += Deactivate;
            moveHandler.ArrivedInSegmentEvent -= TryActivate;
        } else {
            Debug.Log(gameObject.name + " not activated");
        }
    }

    protected virtual void Deactivate() {
        Debug.Log("Disabling " + gameObject.name);
        GameManager.Instance.MoveToNextSegmentEvent -= Deactivate;
        active = false;
    }
    
    protected virtual void Activate() {
        Debug.Log("Activating " + gameObject.name);
        active = true;
    }
}