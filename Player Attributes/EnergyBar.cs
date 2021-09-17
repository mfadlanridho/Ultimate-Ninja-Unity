using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyBar : MonoBehaviour {
    [SerializeField] Image slider;
    [SerializeField] CanvasGroup abilityCanvas;

    [SerializeField] float maxEnergy = 5;
    float energy = 0;

    private void Start() {
        ComboAttack comboAttack = GameObject.FindWithTag("Player").GetComponent<ComboAttack>();
        comboAttack.OnCombo += OnComboPerformed;

        EnemyDetector enemyDetector = FindObjectOfType<EnemyDetector>();
        enemyDetector.OnAfterRemoveEnemy += IncreaseEnergy;
        ResetEnergy();
    }

    private void OnDisable() {
        ComboAttack comboAttack = GameObject.FindWithTag("Player").GetComponent<ComboAttack>();
        comboAttack.OnCombo -= OnComboPerformed;
    }

    void IncreaseEnergy() {
        SetEnergy(energy + 1);
    }

    void ActivateAbility() {
        SetAbilityActivation(true);
    }

    void DeactivateAbility() {
        SetAbilityActivation(false);
    }

    void SetAbilityActivation(bool isActive) {
        abilityCanvas.interactable = isActive;
        abilityCanvas.alpha = isActive ? 1f : .5f;
    }

    void SetEnergy(float val) {
        if (val > maxEnergy)
            return;
        
        energy = val;

        float fillAmount = energy / maxEnergy;
        slider.DOFillAmount(fillAmount, .25f);

        if (energy == maxEnergy) {
            ActivateAbility();
        }
    }

    void ResetEnergy() {
        SetEnergy(0);
    }

    void OnComboPerformed() {
        DeactivateAbility();
        ResetEnergy();
    }
}