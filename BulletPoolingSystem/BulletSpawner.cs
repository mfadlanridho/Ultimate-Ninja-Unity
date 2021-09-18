using UnityEngine;

namespace BulletPoolingSystem {
public class BulletSpawner : MonoBehaviour {
    [SerializeField] Transform muzzle;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            BulletPool.Instance.Spawn(muzzle.position, muzzle.rotation, 10f);
        }
    }
}
}