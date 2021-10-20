using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[System.Serializable]
public class SkillHolder : MonoBehaviour {
    [SerializeField] PlayerSkill playerSkill;
    [SerializeField] int price;
    
    [Space]
    [SerializeField] Button button;
    [SerializeField] Image lockImage;

    public PlayerSkill Skill => playerSkill;
    public Button Button => button;
    public int Price => price;
    public bool Purchased => playerSkill.Purchased;

    Animator animator;

    public System.Action<SkillHolder> OnSkillSelected;
    public System.Action<SkillHolder> OnSkillAccessed;

    void OnEnable() {
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        
        // playerSkill.ResetPurchase();
        playerSkill.OnAccess += AllowInteraction;
        playerSkill.OnPurchase += DisableLockImage;

        button.onClick.AddListener(Animate);
        button.onClick.AddListener(ButtonClick);
    }

    void OnDisable () {
        playerSkill.OnPurchase -= DisableLockImage;
        playerSkill.OnAccess -= AllowInteraction;

        button.onClick.RemoveListener(Animate);
    }

    public void Purchase() {
        playerSkill.Purchase();
    }
    
    void Animate() {
        playerSkill.Animate(animator);
    }

    void AllowInteraction() {
        button.interactable = true;
        if (OnSkillAccessed != null) {
            OnSkillAccessed(this);
        }
    }

    void DisableLockImage() {
        lockImage.gameObject.SetActive(false);
    }

    void ButtonClick() {
        if (OnSkillSelected != null) {
            OnSkillSelected(this);
        }
    }
}