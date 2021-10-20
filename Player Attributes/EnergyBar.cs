using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyBar : MonoBehaviour {
    [SerializeField] Image slider;
    [SerializeField] CanvasGroup abilityCanvas;

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
        if (val > 3f)
            return;
        
        energy = val;

        float fillAmount = energy / 3f;
        slider.DOFillAmount(fillAmount, .25f);

        if (energy == 3f) {
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