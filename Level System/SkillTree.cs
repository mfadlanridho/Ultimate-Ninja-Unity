using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using TMPro;

public class SkillTree : MonoBehaviour {
    [SerializeField] Button purchaseButton;
    [SerializeField] Color selectedColor;
    [SerializeField] Color purchasedColor;
    [SerializeField] Color unpurchasedColor;

    [Space]
    [SerializeField] Sound purchaseSound;
    [SerializeField] Sound notEnoughPointsSound;

    [Space]
    [SerializeField] TextMeshProUGUI AvailableStarsText;
    [SerializeField] TextMeshProUGUI TotalStarsText;
    [SerializeField] TextMeshProUGUI priceText;

    [Space]
    [SerializeField] SkillTypeHoldersDictionary skillHolders;

    SkillHolder selectedSkill;

#region Unity methods
    void OnEnable() {
        purchaseButton.onClick.AddListener(TryPurchasing);

        foreach (KeyValuePair<SkillType, SkillHolderContainer> skills in skillHolders) {
            foreach (SkillHolder skillHolder in skills.Value.SkillHolders) {
                skillHolder.Skill.ResetPurchase();

                skillHolder.OnSkillSelected += SetSkillAsSelected;
                skillHolder.OnSkillAccessed += AccessSkill;
            }
        }
        PurchasePurchasedSkill();
        UpdateStarsText();
    }

    void OnDisable() {
        purchaseButton.onClick.RemoveListener(TryPurchasing);

        foreach (KeyValuePair<SkillType, SkillHolderContainer> skills in skillHolders) {
            foreach (SkillHolder skill in skills.Value.SkillHolders) {
                skill.OnSkillSelected -= SetSkillAsSelected;
                skill.OnSkillAccessed -= AccessSkill;
            }
        }
    }

    void TryPurchasing() {
        bool success = PlayerStats.Instance.AvailableStarPoints - selectedSkill.Price >= 0;
        if (success) {
            Purchase();
        } else {
            AudioManager.Instance.Play(notEnoughPointsSound.Audio, notEnoughPointsSound.Volume);
            #if UNITY_EDITOR
            Debug.Log("Not enough stars");
            #endif
        }
    }
#endregion

#region public
    public void DisablePurchaseButton() {
        purchaseButton.gameObject.SetActive(false);
        priceText.gameObject.SetActive(false);
    }
#endregion

    void Purchase(bool actualPurchase = true) {        
        if (actualPurchase) {
            AudioManager.Instance.Play(purchaseSound.Audio, purchaseSound.Volume);
            PlayerStats.Instance.UseStar(selectedSkill.Price);
            PlayerStats.Instance.IncreaseAttackCount(selectedSkill.Skill.SkillType);
            UpdateStarsText();
        }
        
        selectedSkill.Purchase();
        UpdateSkillSequence(selectedSkill);
        SetSelectedSkillToNull();
        
        FindObjectOfType<SaveManager>().Save();
    }

    void SetSelectedSkillToNull() {
        selectedSkill = null;
        DisablePurchaseButton();
    }

    void UpdateSkillSequence(SkillHolder finalSkill) {
        SkillHolder holder = finalSkill;
        while (holder != null) {
            SetColor(holder.Button, purchasedColor);

            holder = holder.Skill.PreviousSkill?.Holder;
        }
    }

    void SetColor(Button button, Color color) {
        button?.GetComponent<Image>().DOColor(color, .5f);
    }
    
    void SetSkillAsSelected(SkillHolder newSkill) {
        if (selectedSkill != null) {
            SetColor(selectedSkill.Button, selectedSkill.Purchased ? purchasedColor : unpurchasedColor);
        }
        
        selectedSkill = newSkill;
        SetColor(newSkill.Button, selectedColor);
        
        if (newSkill.Purchased) {
            DisablePurchaseButton();
        } else {
            EnablePurchaseButton();
        }
    }

    void AccessSkill(SkillHolder skillHolder) {
        SetColor(skillHolder.Button, unpurchasedColor);
    }

    void EnablePurchaseButton() {
        purchaseButton.gameObject.SetActive(true);
        priceText.gameObject.SetActive(true);
        priceText.text = selectedSkill.Price.ToString() + " stars";
    }

    void PurchasePurchasedSkill() {
        for (int i = 0; i < PlayerStats.Instance.BasicAttackCount; i++) {
            var skill = skillHolders[SkillType.BasicAttack].SkillHolders[i];
            
            SetSkillAsSelected(skill);
            Purchase(false);
        }

        for (int i = 0; i < PlayerStats.Instance.ComboAttackCount; i++) {
            var skill = skillHolders[SkillType.ComboAttack].SkillHolders[i];
            
            SetSkillAsSelected(skill);
            Purchase(false);
        }
    }

    void UpdateStarsText() {
        TotalStarsText.text = PlayerStats.Instance.StarPoints.ToString();
        AvailableStarsText.text = (PlayerStats.Instance.StarPoints - PlayerStats.Instance.UsedStars).ToString();
    }
}

[System.Serializable]
public class SkillHolderContainer {
    public SkillHolder[] SkillHolders;
}