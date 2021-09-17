using UnityEngine;

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
        if (Mathf.Abs(keyboardHorizontal) >= .1f) {
            return Input.GetAxis("Horizontal");
        }
        return UltimateJoystick.GetHorizontalAxis("Movement");
    }

    public float GetVertical() {
        float keyboardVertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(keyboardVertical) >= .1f) {
            return keyboardVertical;
        }
        return UltimateJoystick.GetVerticalAxis("Movement");
    }
}