using UnityEngine;

public class CrateTarget : MonoBehaviour {
    Collider[] crate = new Collider[1];

    private CrateMover ContainsCrate() {
        crate[0] = null;
        Physics.OverlapSphereNonAlloc(transform.position + transform.up, .1f, crate, LayerMask.GetMask("Crate"));
        
        if (crate[0] != null) { 
            return crate[0].GetComponent<CrateMover>();
        } else {
            return null;
        }
    }

    private void Update() {
        CrateMover crate = ContainsCrate();
        if (crate != null) {
            if (Vector3.SqrMagnitude(crate.transform.position - transform.position) < .25f)
                crate.AtTarget();
        }
    }
}