using System;
using UnityEngine;
using UnityEngine.UI;
using SegmentPoolingSystem;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> {}

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> {}

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> {}

[Serializable]
public class RangePositionsDictionary : SerializableDictionary<EnemyRange, AIPosition> {}

[Serializable]
public class EnemySpawnDictionary : SerializableDictionary<EnemyName, EnemySpawner.EnemySpawnConfiguration> {}

[Serializable]
public class EnemyAttributesDictionary : SerializableDictionary<EnemyName, AttributesOfAnEnemy> {}

[Serializable]
public class FloorTypeObjectDictionary : SerializableDictionary<FloorType, FloorObjects> {}

[Serializable]
public class EnemyCountDictionary : SerializableDictionary<EnemyName, int> {}

[Serializable]
public class SkillTypeHoldersDictionary : SerializableDictionary<SkillType, SkillHolderContainer> {}

[Serializable]
public class SkillTypeCountDictionary : SerializableDictionary<SkillType, int> {}

[Serializable]
public class TrapPoolDictionary : SerializableDictionary<TrapSpawner.Type, TrapSpawner.Pool> {}

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> {}

[Serializable]
public class GameObjectStorage : SerializableDictionary.Storage<GameObject[]> {}