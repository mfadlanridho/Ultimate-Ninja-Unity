using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SegmentPoolingSystem {
public static class GameConfiguration {
    public static int LevelIndex {get; private set;}

    public static float MaxHealth {get; private set;}
    public static float Speed {get; private set;}
    public static float Damage {get; private set;}

    public static int StarPoints {get; private set;}
    public static int UsedStars {get; private set;}
    public static int AvailableStarPoints {get {return StarPoints - UsedStars;}}

    public static void SetLevel(int value) {
        LevelIndex = value;
    }

    public static void SetMaxHealth(float value) {
        MaxHealth = value;
    }

    public static void SetSpeed(float value) {
        Speed = value;
    }

    public static void SetDamage(float value) {
        Damage = value;
    }

    public static void SetStarPoints(int value) {
        StarPoints = value;
    }

    public static void UseStar(int count = 1) {
        UsedStars += count;
    }
}
}