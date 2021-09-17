using UnityEngine;
using DG.Tweening;

public class GridMover : MonoBehaviour {
    [SerializeField] RectTransform leftArrow;
    [SerializeField] RectTransform rightArrow;

    [Space]
    [SerializeField] RectTransform[] grids;
    [SerializeField] float distance = 70;
    [SerializeField] float duration = 1f;
    
    SkillTree skillTree;

    int curIndex;
    bool moving;

    private void Start() {
        skillTree = GetComponent<SkillTree>();
        CheckAvailableDirections();
    }

    public void MoveRight() {
        if (moving)
            return;
        
        skillTree.DisablePurchaseButton();

        moving = true;
        curIndex = (curIndex + 1) % grids.Length;

        transform.DOLocalMoveX(transform.localPosition.x - distance, duration)
        .OnComplete(StopMoving);

        CheckAvailableDirections();
    }

    public void MoveLeft() {
        if (moving)
            return;

        skillTree.DisablePurchaseButton();

        moving = true;
        curIndex = curIndex > 0 ? curIndex - 1 : grids.Length - 1;

        transform.DOLocalMoveX(transform.localPosition.x + distance, duration)
        .OnComplete(StopMoving);

        CheckAvailableDirections();
    }

    void CheckAvailableDirections() {
        leftArrow.gameObject.SetActive(curIndex != 0);
        rightArrow.gameObject.SetActive(curIndex != grids.Length -1);
    }

    void StopMoving() {
        moving = false;
    }
}