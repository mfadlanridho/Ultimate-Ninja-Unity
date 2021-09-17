using UnityEngine;
using Cinemachine;

public class GameplayCamera : MonoBehaviour {
    private void Start() {
        CinemachineVirtualCamera vmCam = GetComponent<CinemachineVirtualCamera>();

        Transform player = GameObject.FindWithTag("Player").transform;
        player.position = Vector3.zero;
        vmCam.Follow = player;

        player.GetComponent<ComboAttack>().SetVMCam(vmCam);
    }
}