using UnityEngine;

public abstract class LevelAttribute : MonoBehaviour {
    [SerializeField] protected int levelToUnlock;
    LevelSystem levelSystem;

    bool unlocked;

    protected virtual void Start() {
        levelSystem = GetComponent<LevelSystem>();
        levelSystem.AfterLevelUp += CheckUnlockAttribute;
    }
    protected abstract void UnlockAbility();

    void Unlock() {
        unlocked = true;
        Debug.Log("Unlocked");
        UnlockAbility();
    }

    void CheckUnlockAttribute() {
        if (levelSystem.Level >= levelToUnlock) {
            Unlock();
        }
    }
}