using UnityEngine;
using DG.Tweening;
using TMPro;

namespace SegmentPoolingSystem {
public class SegmentMoveHandler : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moveWarningText;
    [SerializeField] Transform btEdges;
    [SerializeField] Transform lEdge;
    [SerializeField] Transform rEdge;

    bool moving;
    Transform player;

    public System.Action ArrivedInSegmentEvent;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        
        GameManager.Instance.MoveToNextSegmentEvent += StartMoving;
    }

    void StartMoving() {
        moving = true;
        moveWarningText?.gameObject.SetActive(true);

        rEdge?.DOMoveY(-1, 1f).OnComplete(delegate{rEdge.gameObject.SetActive(false);});
    }

    void StopMoving() {
        moving = false;
        moveWarningText?.gameObject.SetActive(false);

        float targetX = GameManager.Instance.Increment * GameManager.Instance.CurrentSegment;
        btEdges?.DOMoveX(targetX, 1.75f);

        rEdge.gameObject.SetActive(true);
        lEdge.position = new Vector3(targetX - 10f, -1f, lEdge.position.z);
        rEdge.position = new Vector3(targetX + 10f, rEdge.position.y, rEdge.position.z);

        DOTween.Sequence()
        .Append(lEdge?.DOMoveY(0f, 1f));

        DOTween.Sequence()
        .Append(rEdge?.DOMoveY(0f, 1f));
    }

    void Update() {
        if (moving) {
            if (Mathf.FloorToInt(player.position.x) 
            > GameManager.Instance.Increment * GameManager.Instance.CurrentSegment - 10f) {
                StopMoving();
                if (ArrivedInSegmentEvent != null) {
                    ArrivedInSegmentEvent();
                }
            } else {
                Vector3 targetPos = Vector3.right * player.position.x;
                btEdges.transform.position = targetPos;
            }
        }
    }
}
}