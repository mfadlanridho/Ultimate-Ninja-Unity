using UnityEngine;

public class ParticleSystemMuzzleFire : MonoBehaviour {
    [SerializeField] ParticleSystem[] particleSystems;

    FirearmWeapon weapon;
    int curIndex;

    private void Start() {
        weapon = GetComponent<FirearmWeapon>();
        weapon.OnFire += GenerateMuzzleFire;
    }

    void GenerateMuzzleFire() {
        particleSystems[curIndex].Play();
        curIndex = (curIndex + 1) % particleSystems.Length;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerateMuzzleFire();
        }
    }
}