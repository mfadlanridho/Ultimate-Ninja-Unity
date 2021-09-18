using UnityEngine;
using DG.Tweening;
using SegmentPoolingSystem;

public class EdgeMove : MonoBehaviour {
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;
    [SerializeField] Transform topBotEdges;
    [SerializeField] float duration = 4f; 
    [SerializeField] GameObject moveWarning;
    [SerializeField] bool alwaysMoving;
    // [SerializeField] Sound moveWarningSound;
    
    Transform player;
    public bool Moving {get; private set;}

    float currentTargetXPosition;
    float finalXTarget;

    public System.Action OnFinalPosition;

    Vector3 initialLPos;
    Vector3 initialTopBotPos;
    bool complete;

    bool movingEdgeDown;

    public System.Action OnArrivedInPosition;

    private void Awake() {
        if (alwaysMoving) 
            Moving = true;

        initialLPos = leftEdge.position;
        initialTopBotPos = topBotEdges.position;
    }

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        finalXTarget = 0;
        // MapAttributes.Instance.FloorCountToBeSpawned 
        //     * MapAttributes.Instance.XIncrement;   
    }

    void Update() {
        if (Moving) {
            int curXPos = Mathf.FloorToInt(transform.position.x) ;

            Vector3 nextLEdgePos = initialLPos + Vector3.right * curXPos;
            leftEdge.position = nextLEdgePos.x > initialLPos.x ? nextLEdgePos : leftEdge.position;
            topBotEdges.position = initialTopBotPos + Vector3.right * curXPos;
            
            if (curXPos >= Mathf.FloorToInt(currentTargetXPosition)) {
                if (curXPos >= Mathf.FloorToInt(finalXTarget)) {
                    if (OnFinalPosition != null) {
                        OnFinalPosition();
                        complete = true;
                    }
                } else {
                    if (OnArrivedInPosition != null) {
                        OnArrivedInPosition();
                    }
                }
                SetMoving(false);
            }
        }
    }

    void SetMoving(bool value) {
        if (alwaysMoving)
            return;
        
        moveWarning?.SetActive(value);

        if (value == true) {
            Animate(moveWarning.transform);
            MoveDownLREdges();
            // AudioManager.Instance.Play(moveWarningSound.Audio, Camera.main.transform.position, moveWarningSound.Volume);
        }
        else {
            MoveUpLREdges();
            Moving = false;
        }
    }

    public bool InPosition() {
        return !Moving ^ movingEdgeDown;
    }

    public void Move() {
        // currentTargetXPosition += MapAttributes.Instance.XIncrement;
        SetMoving(true);
    }

    void Animate(Transform transform) {
        transform.localScale = Vector3.one * 1.5f;
        transform.DOScale(1, .5f);
    }

    void MoveDownLREdges() {
        movingEdgeDown = true;
        if ((int)leftEdge.transform.position.x != (int)initialLPos.x)
            leftEdge.transform.DOMoveY(-1.5f, 1f);
        rightEdge.transform.DOMoveY(-1.5f, 1f).OnComplete(SetMovingToTrue);
    }

    void MoveUpLREdges() {
        leftEdge.transform.DOMoveY(0, 1f);
        rightEdge.transform.DOMoveY(0, 1f);
    }

    void SetMovingToTrue() {
        // rightEdge.transform.position += Vector3.right * MapAttributes.Instance.XIncrement;
        Moving = true;

        movingEdgeDown = false;
    }
}