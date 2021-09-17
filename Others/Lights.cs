using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Lights : MonoBehaviour {
    [SerializeField] public Light[] Lamps;

    [Space]
    [SerializeField] bool isFlickeringLights;
    [SerializeField] Vector2 durationRange;

    private void Start() {
        if (isFlickeringLights) {
            StartCoroutine(FlickeringLights());
        }

        // if (!MapAttributes.Instance.IsNightTime) {
        //     foreach (Light lamp in Lamps) {
        //         lamp.intensity = 0;
        //     }
        // } else if (isFlickeringLights) {
        //     foreach (Light lamp in Lamps) {
        //         lamp.intensity = 1f;
        //     }
        // }
    }

    IEnumerator FlickeringLights() {
        foreach (Light lamp in Lamps) {
            lamp.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(Random.Range(durationRange.x, durationRange.y));

        foreach (Light lamp in Lamps) {
            lamp.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(Random.Range(durationRange.x, durationRange.y));
        
        StartCoroutine(FlickeringLights());
    }
}