using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(RangePositionsDictionary))]
[CustomPropertyDrawer(typeof(EnemySpawnDictionary))]
[CustomPropertyDrawer(typeof(EnemyAttributesDictionary))]
[CustomPropertyDrawer(typeof(FloorTypeObjectDictionary))]
[CustomPropertyDrawer(typeof(EnemyCountDictionary))]
[CustomPropertyDrawer(typeof(SkillTypeHoldersDictionary))]
[CustomPropertyDrawer(typeof(SkillTypeCountDictionary))]
[CustomPropertyDrawer(typeof(TrapPoolDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
[CustomPropertyDrawer(typeof(GameObjectStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}