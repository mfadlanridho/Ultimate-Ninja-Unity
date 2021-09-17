using UnityEngine;

[System.Serializable]
public class Sound {
    public AudioClip Audio;
    [Range(0 ,1)] public float Volume = 1f;
}