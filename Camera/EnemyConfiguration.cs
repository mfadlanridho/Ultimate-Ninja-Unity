using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "EnemyConfiguration")]
public class EnemyConfiguration : ScriptableObject {
    // public EnemyAttributesDictionary enemyAttributesDictionary;
    // public IDictionary<EnemyName, AttributesOfAnEnemy> EnemyAttributesDictionary
	// {
	// 	get { return enemyAttributesDictionary; }
	// 	set { enemyAttributesDictionary.CopyFrom (value); }
	// }

    // public void RegenerateEnemyNames() {
    //     EnemyAttributesDictionary.Clear();
    //     foreach (EnemyName name in System.Enum.GetValues(typeof(EnemyName)).Cast<EnemyName>()) {
    //         EnemyAttributesDictionary[name] = new AttributesOfAnEnemy();
    //     }
    // }
}

[System.Serializable]
public enum EnemyName {
    Sword1,
    Sword2,
    Sword3,
    GreatSword1,
    GreatSword2,
    GreatSword3,
    Rifle1,
    Rifle2,
    Rifle3,
    Minigun1,
    Minigun2,
    Minigun3,
    Longsword1,
    Longsword2,
    Longsword3,
    Archer1,
    Archer2,
    Archer3
}

[System.Serializable]
public class AttributesOfAnEnemy {
    public string Name;
    public float MaxHealth;
    public float Damage;
    public float Speed;
}