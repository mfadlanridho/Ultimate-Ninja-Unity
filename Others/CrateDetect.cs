using UnityEngine;

public class CrateDetect : MonoBehaviour {
    Animator animator;
    bool pushing;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        DetectCrate();
    }

    Collider[] crate = new Collider[1];
    private void DetectCrate() {
        crate[0] = null;
        Physics.OverlapSphereNonAlloc(transform.position + transform.forward * 1f, .5f, crate, LayerMask.GetMask("Crate"));
        
        if (crate[0] != null) { 
            crate[0].GetComponent<CrateMover>().Move(transform);
            animator.SetBool("Pushing", true);
            pushing = true;
        } else if (pushing) {
            animator.SetBool("Pushing", false);
            pushing = false;
        }
    }
}