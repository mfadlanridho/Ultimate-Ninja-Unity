using UnityEngine;

public static class MapData {
    public static EnemyAttributesDictionary EnemyAttributesDictionary = new EnemyAttributesDictionary() {
        { EnemyName.Sword1, new AttributesOfAnEnemy { Name = "Swordsman Lv1", MaxHealth = 200, Damage = 20, Speed = 2 }},
        { EnemyName.Sword2, new AttributesOfAnEnemy { Name = "Swordsman Lv2", MaxHealth = 250, Damage = 30, Speed = 2.5f }},
        { EnemyName.Sword3, new AttributesOfAnEnemy { Name = "Swordsman Lv3", MaxHealth = 300, Damage = 40, Speed = 3 }},

        { EnemyName.Rifle1, new AttributesOfAnEnemy { Name = "Gunman Lv1", MaxHealth = 200, Damage = 7, Speed = 2 }},
        { EnemyName.Rifle2, new AttributesOfAnEnemy { Name = "Gunman Lv2", MaxHealth = 250, Damage = 14, Speed = 2.5f }},
        { EnemyName.Rifle3, new AttributesOfAnEnemy { Name = "Gunman Lv3", MaxHealth = 300, Damage = 21, Speed = 3 }},

        { EnemyName.Longsword1, new AttributesOfAnEnemy { Name = "Longswordsman Lv1", MaxHealth = 175, Damage = 25, Speed = 2 }},
        { EnemyName.Longsword2, new AttributesOfAnEnemy { Name = "Longswordsman Lv2", MaxHealth = 225, Damage = 35, Speed = 2.5f }},
        { EnemyName.Longsword3, new AttributesOfAnEnemy { Name = "Longswordsman Lv3", MaxHealth = 275, Damage = 35, Speed = 3 }},

        { EnemyName.Minigun1, new AttributesOfAnEnemy { Name = "Minigunman Lv1", MaxHealth = 175, Damage = 5, Speed = 1.5f }},
        { EnemyName.Minigun2, new AttributesOfAnEnemy { Name = "Minigunman Lv2", MaxHealth = 225, Damage = 10, Speed = 2 }},
        { EnemyName.Minigun3, new AttributesOfAnEnemy { Name = "Minigunman Lv3", MaxHealth = 275, Damage = 15, Speed = 2.5f }},

        { EnemyName.GreatSword1, new AttributesOfAnEnemy { Name = "Greatswordsman Lv1", MaxHealth = 300, Damage = 40, Speed = 1.5f }},
        { EnemyName.GreatSword2, new AttributesOfAnEnemy { Name = "Greatswordsman Lv2", MaxHealth = 400, Damage = 50, Speed = 2 }},
        { EnemyName.GreatSword3, new AttributesOfAnEnemy { Name = "Greatswordsman Lv3", MaxHealth = 500, Damage = 60, Speed = 2.5f }},
    };

    public static int GetFloorCountToBeSpawned(int levelIndex) {
        return 2 * Mathf.FloorToInt(levelIndex/ 5 ) + 3;
    }
}