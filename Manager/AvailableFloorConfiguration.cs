using UnityEngine;

[CreateAssetMenu(fileName = "AvailableFloorConfiguration")]
public class AvailableFloorConfiguration : ScriptableObject {
    public FloorType[] FloorTypes = 
    {
        FloorType.Default,
        FloorType.Spikes1,
        FloorType.Flames1,
        FloorType.Lasers1,
        FloorType.Turrets1,
        FloorType.SpikesxFlames1,
        FloorType.FlamesxLasers1,
        FloorType.LasersxTurrets1,
        FloorType.SpikesxLasers1,
        FloorType.SpikesxTurrets1,
        FloorType.FlamesxTurrets1,
        FloorType.Spikes2,
        FloorType.Flames2,
        FloorType.Lasers2,
        FloorType.Turrets2,
        FloorType.SpikesxFlames2,
        FloorType.FlamesxLasers2,
        FloorType.LasersxTurrets2,
        FloorType.SpikesxLasers2,
        FloorType.SpikesxTurrets2,
        FloorType.FlamesxTurrets2,
        FloorType.Spikes3,
        FloorType.Flames3,
        FloorType.Lasers3,
        FloorType.Turrets3,
        FloorType.SpikesxFlames3,
        FloorType.FlamesxLasers3,
        FloorType.LasersxTurrets3,
        FloorType.SpikesxLasers3,
        FloorType.SpikesxTurrets3,
        FloorType.FlamesxTurrets3
    };
}