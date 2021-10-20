using UnityEngine;
using Cinemachine;
using DG.Tweening;
using SegmentPoolingSystem;

public class MapStart : MonoBehaviour {
    [SerializeField] CinemachineVirtualCamera vmCam;
    float forwardDuration = .75f;
    float backDuration = .5f;

    public bool Complete {get; private set;}
    public System.Action OnComplete;

    private void Start() {
        LevelLoader.Instance.OnAnimationEnd += MoveCamera;
        GameObject.FindWithTag("Player").transform.position = Vector3.zero;
    }

    private void OnDisable() {
        LevelLoader.Instance.OnAnimationEnd -= MoveCamera;
    }

    void MoveCamera() {
        vmCam.gameObject.SetActive(true);
        float floorCount = GameManager.Instance.Increment;
        float finalXTarget = floorCount 
            * GameManager.Instance.Increment;   
        
        DOTween.Sequence().
        AppendInterval(.5f).
        // Append(vmCam.transform.DOMoveX(finalXTarget, forwardDuration * floorCount).SetEase(Ease.Linear)).
        Append(vmCam.transform.DOMoveX(finalXTarget, 2).SetEase(Ease.Linear)).
        AppendInterval(1f).
        // Append(vmCam.transform.DOMoveX(0f, backDuration * floorCount).SetEase(Ease.Linear).OnComplete(DeactivateCamera));
        Append(vmCam.transform.DOMoveX(0f, 2).SetEase(Ease.Linear).OnComplete(DeactivateCamera));
    }

    void DeactivateCamera() {
        vmCam.gameObject.SetActive(false);
        Invoke("Completed", .5f);
    }

    void Completed() {
        Complete = true;
        if (OnComplete != null) {
            OnComplete();
        }
    }
}