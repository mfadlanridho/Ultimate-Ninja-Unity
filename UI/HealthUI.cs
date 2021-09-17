using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HealthUI : MonoBehaviour {
    [SerializeField] GameObject uiPrefab;
    [SerializeField] Vector3 offset;
    [SerializeField] Enemy enemy;
    
    Transform ui;
    CanvasGroup canvasGroup;
    Image healthSlider;
    Transform cam;

    private void Start() {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                canvasGroup = ui.GetComponent<CanvasGroup>();

                ui.Find("name").GetComponent<TextMeshProUGUI>().text 
                    = MapData.EnemyAttributesDictionary[enemy.EnemyName].Name;

                break;
            }
        }
                
        CharacterBase character = GetComponent<CharacterBase>();
        character.AfterHealthChange += UpdateHealthbar;
        character.OnDeath += OnDeath;
        character.OnResetHealth += OnResetHealth;
    }

    void UpdateHealthbar(float currentHealth, float maxHealth) {
        if (ui == null)
            return;

        float healthPercent = currentHealth / maxHealth;
        healthSlider.DOFillAmount(healthPercent, .25f);
    }

    void OnDeath() {
        canvasGroup.alpha = 0;
    }

    void OnResetHealth() {
        UpdateHealthbar(1, 1);
        canvasGroup.alpha = 1;
    }

    private void LateUpdate() {
        if (ui == null)
            return;
        
        ui.position = transform.position + offset;
        ui.forward = -cam.forward;
    }
}