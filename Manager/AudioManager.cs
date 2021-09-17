using UnityEngine;

public class AudioManager : MonoBehaviour {
    static AudioManager instance;

    Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }

    public static AudioManager Instance {
        get {
            if(instance == null) {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;        
        }
    }

    private void Awake()  {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this; 
    }

    public void Play(AudioClip clip, float volume = 1, Vector3 pos = default(Vector3)) {
        if (pos == default(Vector3)) {
            if (cam == null) {
                cam = Camera.main.transform;
            }
            pos = cam.position;
        }

        if (clip != null) {
			AudioSource.PlayClipAtPoint(clip, pos, volume);
		}
    }
}