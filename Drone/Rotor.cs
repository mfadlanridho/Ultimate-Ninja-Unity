using UnityEngine;

public class Rotor : MonoBehaviour {
    [SerializeField] Transform rotor;
    [SerializeField] float speed;

    private void Update() {
        rotor.Rotate(0, 0, Time.deltaTime * speed);
    }
}