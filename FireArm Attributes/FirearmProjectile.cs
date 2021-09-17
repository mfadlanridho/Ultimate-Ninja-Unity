using UnityEngine;

public class FirearmProjectile : MonoBehaviour {
    [SerializeField] float speed = 100f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float raycastAdvance = 1f;

    float damage;

    public System.Action<Vector3> OnHit;
    float time;

    private void Awake() {
        time = lifeTime;
    }

    private void Update() {
        time -= Time.deltaTime;

        if (time < 0) {
            Destroy(gameObject);
            return;
        }

        Vector3 step = transform.forward * Time.deltaTime * speed;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, step.magnitude * raycastAdvance, LayerMask.GetMask("Player", "Enemy"))) {
            if (OnHit != null) {
                OnHit(hit.point);
            }
            hit.transform.GetComponent<CharacterBase>().TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}