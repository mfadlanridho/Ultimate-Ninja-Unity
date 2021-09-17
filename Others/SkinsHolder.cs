using UnityEngine;

public class SkinsHolder : MonoBehaviour {
    public PlayerSkin[] Skins;

    private void Start() {
        Skins = GameObject.FindWithTag("Player").GetComponentsInChildren<PlayerSkin>(true);
        Debug.Log(Skins.Length);
        System.Array.Sort(Skins,
            delegate(PlayerSkin x, PlayerSkin y) { return x.SkinIndex.CompareTo(y.SkinIndex); });
        
        foreach (int index in PlayerStats.Instance.UnlockedSkinsIndexes) {
            Skins[index].Purchase();
        }
    }
}