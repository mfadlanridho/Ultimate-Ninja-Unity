using UnityEngine;

public class HealthRestore : MonoBehaviour {
    [SerializeField] float amount;
    [SerializeField] float waitDuration = 4f;

    bool picked;

    Collider[] player = new Collider[1];

    private void Update() {
        if (waitDuration > 0) {
            waitDuration -= Time.deltaTime;
        } else if (!picked) {
            player = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Player"));
            if (player.Length > 0 && player[0] != null) {
                picked = true;
                player[0].GetComponent<Player>()?.RestoreHealth(amount);
                Destroy(gameObject);
            }
        }
    }
}