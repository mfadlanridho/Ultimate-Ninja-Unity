using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkill")]
public class PlayerSkill : ScriptableObject {
    [SerializeField] SkillType skillType;
    [SerializeField] PlayerSkill[] nextSkills;
    
    [SerializeField] public string Name;
    [SerializeField] public float Damage;
    [SerializeField] public string Desc;

    public System.Action OnPurchase;
    public System.Action OnAccess;

    [SerializeField] public bool Purchased;
    public bool Accessible {get; private set;}
    public PlayerSkill PreviousSkill {get; private set;}
    public SkillHolder Holder {get; private set;}

    public SkillType SkillType {get{return skillType;}}
    public PlayerSkill[] NextSkills {get{return nextSkills;}}

    public void Animate(Animator animator) {
        animator.Play(name);
    }

    public void Purchase() {                
        if (OnPurchase != null) {
            OnPurchase();
        }
        Purchased = true;

        if (nextSkills != null) {
            foreach (PlayerSkill nextSkill in nextSkills) {
                nextSkill.Access();
                nextSkill.SetPreviousSkill(this);
            }
        }
    }

    public void ResetPurchase() {
        Purchased = false;
        Accessible = false;
    }

    void Access() {
        if (OnAccess != null) {
            OnAccess();
        }
        Accessible = true;
    }

    public void SetPreviousSkill(PlayerSkill previousSkill) {
        PreviousSkill = previousSkill;
    }

    public void SetHolder(SkillHolder holder) {
        Holder = holder;
    }
}

public enum SkillType {
    BasicAttack,
    ComboAttack
}