using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SegmentPoolingSystem {
public static class GameConfiguration {
    public static int LevelIndex {get; private set;}

    public static void SetLevelIndex(int value) {
        LevelIndex = value;
    }
}
}