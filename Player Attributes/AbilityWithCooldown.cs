using UnityEngine;

public abstract class AbilityWithCooldown : MonoBehaviour {
    [SerializeField] float duration;
    [HideInInspector] public float OnCooldown;
}