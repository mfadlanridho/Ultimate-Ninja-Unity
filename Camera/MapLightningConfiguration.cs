using UnityEngine;

public class MapLightningConfiguration : MonoBehaviour {
    [SerializeField] Light directionalLight;

    [SerializeField] float dayIntensity;
    [SerializeField] float nightIntensity;
    
    [Header("Skyboxes")]
    [SerializeField] Material daySkybox;
    [SerializeField] Material nightSkybox;

    // private void Start() {
    //     if (MapAttributes.Instance.IsNightTime) {
    //         RenderSettings.skybox = nightSkybox;
    //         directionalLight.intensity = nightIntensity;
    //     } else {
    //         RenderSettings.skybox = daySkybox;
    //         directionalLight.intensity = dayIntensity;
    //     }
    // }
}