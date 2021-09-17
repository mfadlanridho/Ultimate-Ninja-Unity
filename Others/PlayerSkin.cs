using UnityEngine;

public class PlayerSkin : MonoBehaviour {
    public float MaxHealth;
    public float Speed;
    
    [Space]
    public int SkinIndex;
    public int Price;
    public bool Purchased;

    public void Purchase() {
        Purchased = true;
    }
}