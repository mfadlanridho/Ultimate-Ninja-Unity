using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour {
    CinemachineImpulseSource cameraShake;

    private void Start() {
        cameraShake = GetComponent<CinemachineImpulseSource>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDamaged += GenerateShake;
    }

    void GenerateShake() {
        cameraShake.GenerateImpulse(transform.forward);
    }

    private void OnDisable() {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDamaged -= GenerateShake;
    }
}