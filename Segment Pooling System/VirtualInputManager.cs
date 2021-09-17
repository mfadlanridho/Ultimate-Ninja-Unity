using UnityEngine;

namespace SegmentPoolingSystem {
public class VirtualInputManager : MonoBehaviour {
    static VirtualInputManager instance;
    public static VirtualInputManager Instance {
        get {
            if(instance == null) {
                instance = new GameObject("VirtualInputManager").AddComponent<VirtualInputManager>();
            }
            return instance;        
        }
    }

    private void Awake()  {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this; 
    }

    public float GetHorizontal() {        
        float keyboardHorizontal = Input.GetAxis("Horizontal");;
        return Input.GetAxis("Horizontal");
    }

    public float GetVertical() {
        float keyboardVertical = Input.GetAxis("Vertical");
        return keyboardVertical;
    }
}
}