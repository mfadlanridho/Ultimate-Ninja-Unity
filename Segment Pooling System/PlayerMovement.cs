using UnityEngine;

namespace SegmentPoolingSystem {
public class PlayerMovement : MonoBehaviour {
    float speed = 4f;

    private void Update() {
        Vector3 input = new Vector3(VirtualInputManager.Instance.GetHorizontal(), 0, VirtualInputManager.Instance.GetVertical());
        float magnitude = input.magnitude;
        if (magnitude > 0) {
            transform.LookAt(transform.position + input);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
        }
    }
}
}