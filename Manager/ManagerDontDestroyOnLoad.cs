using UnityEngine;

public class ManagerDontDestroyOnLoad : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}