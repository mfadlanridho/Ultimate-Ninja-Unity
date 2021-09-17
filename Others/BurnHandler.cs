using UnityEngine;

public class BurnHandler : MonoBehaviour{
    [SerializeField] Burn burnPrefab;
    [SerializeField] Vector3 burnLocalPos;
    [SerializeField] float duration;

    Burn burn;
    bool burning;
    float time;

    public void Ignite(CharacterBase character) {
        if (burn == null) {
            burn = Instantiate(burnPrefab, transform);
            burn.transform.localPosition = burnLocalPos;
            burn.SetCharacter(character);
        }
        time = duration;
        SetBurning(true);
    }

    private void Update() {
        if (burning && time > 0) {
            time -= Time.deltaTime;
        } else if (burning) {
            SetBurning(false);
        }
    }

    void SetBurning(bool value) {
        burning = value;
        burn?.gameObject.SetActive(value);
    }

    public void StopBurn() {
        SetBurning(false);
    }
}