using UnityEngine;

public class FirearmMuzzleFire : MonoBehaviour {
    FirearmWeapon weapon;
    [SerializeField] GameObject muzzleFire;
    bool showingFire;

    private void Start() {
        muzzleFire.SetActive(false);
        weapon = GetComponent<FirearmWeapon>();
        weapon.OnFire += GenerateMuzzleFire;
    }

    void GenerateMuzzleFire() {
        muzzleFire.SetActive(true);
        showingFire = true;
    }

    private void Update() {
        if (showingFire) {
            showingFire = false;
            muzzleFire.SetActive(false);
        }
    }
}