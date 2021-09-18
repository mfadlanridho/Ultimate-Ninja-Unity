using UnityEngine;

namespace BulletPoolingSystem {
public class BulletHit : MonoBehaviour {
    private void OnParticleSystemStopped() {
        BulletPool.Instance.StoreToPool(this);
    }
}
}