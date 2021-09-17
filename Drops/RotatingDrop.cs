using UnityEngine;

public class RotatingDrop : MonoBehaviour {
    [SerializeField] float speed = 100f;

    private void Update() {
        transform.Rotate(0, Time.deltaTime * speed, 0);
    }
}