using UnityEngine;

public class HealthIncrease : LevelAttribute {
    Player player;

    protected override void Start() {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    void IncreaseMaxHealth(float value) {
        player.IncreaseMaxHealth(value);
    }

    protected override void UnlockAbility() {
        IncreaseMaxHealth(player.MaxHealth / 4);
    }
}