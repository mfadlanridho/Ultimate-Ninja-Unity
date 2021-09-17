using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class ComboAttack : AttackBase {
    [SerializeField] float sizeChangeDuration = 1;
    [SerializeField] float attackOrthoSize = 4;

    [SerializeField] int attackIndex;
    [SerializeField] string[] attackNames;
    Transform player;

    // Camera cam;
    CinemachineVirtualCamera vmCam;
    float ogOrthoSize;
    Vector3 ogEulerAngles;

    int attackCount;

    public void SetVMCam(CinemachineVirtualCamera vmCam) {
        this.vmCam = vmCam;
        ogOrthoSize = vmCam.m_Lens.OrthographicSize;
        ogEulerAngles = vmCam.transform.eulerAngles;
    }

    protected override void Start() {
        base.Start();
        player = GameObject.FindWithTag("Player").transform;
    }

    void InitiateAttack(int index) {
        if ( PlayerStateMachine.Instance.State == PlayerState.None ) {
            FirstAttack(index);
        }
    }

    void FirstAttack(int index) {
        attackCount = 0;
        Attack(attackNames[index], 1f);
        animator.SetBool("ContinueComboAttack", true);
    }


    public void MoveCameraForward() {
        float orthoSize = ogOrthoSize;
        DOTween.To(()=> orthoSize, x=> orthoSize = x, attackOrthoSize, sizeChangeDuration).OnUpdate(() => { 
            vmCam.m_Lens.OrthographicSize = orthoSize;});
    }

    public void MoveCameraBack() {
        float orthoSize = attackOrthoSize;
        DOTween.To(()=> orthoSize, x=> orthoSize = x, ogOrthoSize, sizeChangeDuration).OnUpdate(() => { 
            vmCam.m_Lens.OrthographicSize = orthoSize;});
    }

    public System.Action OnCombo;
    public void DoCombo() {
        if (OnCombo != null)
            OnCombo();
    }

    public void InitiateAttack() {
        InitiateAttack(attackIndex);
    }

    public void IncreaseAttackCount() {
        attackCount++;
        animator.SetBool("ContinueComboAttack", attackCount < PlayerStats.Instance.ComboAttackCount);
    }
}