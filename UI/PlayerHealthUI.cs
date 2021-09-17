using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour {
    [SerializeField] Image healthSlider;

    [Header("Divider")]
    [SerializeField] GameObject divider;
    List<GameObject> dividers = new List<GameObject>();

    Player player;
    int divCount;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.RestoreHealth(1000);
        player.AfterHealthChange += UpdateUI;
        player.AfterHealthRestore += UpdateUI;
        player.AfterHealthIncrease += CheckUpdateDivider;
        SetDivider();
    }

    private void OnDisable() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.AfterHealthChange -= UpdateUI;
        player.AfterHealthRestore -= UpdateUI;
        player.AfterHealthIncrease -= CheckUpdateDivider;
    }

    void UpdateUI(float currentHealth, float maxHealth) {
        float healthPercent = currentHealth / maxHealth;
        healthSlider.DOFillAmount(healthPercent, .25f);
    }

    void SetDivider() {
        ClearDividers();

        divCount = Mathf.FloorToInt(player.MaxHealth / 25);
        float percentDiff = 1f / (float)divCount;
        float posDiff = 1162 * percentDiff;

        for (int i = 1; i <= divCount; i++) {
            float pos = posDiff * i;
            RectTransform rect = Instantiate(divider, healthSlider.transform).GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(pos, 0, 0);
            dividers.Add(rect.gameObject);
        }
    }

    void CheckUpdateDivider() {
        int divCheck = Mathf.FloorToInt(player.MaxHealth / 25);
        if (divCheck != divCount) {
            SetDivider();
        }
    }

    void ClearDividers() {
        foreach (GameObject d in dividers) {
            Destroy(d);
        }
        dividers.Clear();
    }

}