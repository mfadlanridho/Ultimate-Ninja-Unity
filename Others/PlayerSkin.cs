using UnityEngine;

public class PlayerSkin : MonoBehaviour {
    public float MaxHealth;
    public float Speed;
    
    [Space]
    public int SkinIndex;
    public int Price;
    public bool Purchased {get; private set;}

    public void Purchase() {        
        Purchased = true;

        if (!PlayerStats.Instance.UnlockedSkinsIndexes.Contains(SkinIndex))
            PlayerStats.Instance.UnlockedSkinsIndexes.Add(SkinIndex);
    }
}