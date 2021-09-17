using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHitVignette : MonoBehaviour {
    [SerializeField] float lerpBackTime = 1f;
    [SerializeField] float value = .3f;

    VolumeProfile postProcessing;
    Player player;

    bool lerpingBack;
    float time;

    private void Start() {
        Volume volume = FindObjectOfType<Volume>();
        if(volume == null) {
            this.enabled = false;
        } else {
            postProcessing = FindObjectOfType<Volume>().profile;
            GameObject.FindWithTag("Player").GetComponent<Player>().AfterHealthChange += OnDamage;
        }
    }

    private void OnDisable() {
        GameObject.FindWithTag("Player").GetComponent<Player>().AfterHealthChange -= OnDamage;
    }

    void OnDamage(float currentHealth, float maxHealth) {
        lerpingBack = true;
        time = lerpBackTime;
        ChangeValue(value);
    }

    private void Update() {
        if (lerpingBack && time > 0) {
            time -= Time.deltaTime;
            ChangeValue( (time/lerpBackTime) * value);
        } else if (lerpingBack) {
            lerpingBack = false;
            ChangeValue(0);
        }
    }

    void ChangeValue(float value) {
        Vignette vignette;
        if (postProcessing.TryGet(out vignette)) {
            vignette.intensity.value = value;
        }
    }
}