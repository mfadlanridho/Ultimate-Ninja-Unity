using UnityEngine;
using TMPro;

public class DistanceCounter : MonoBehaviour {
    Transform player;
    [SerializeField] TextMeshProUGUI text;

    float initialPosition;
    float currentPosition {
        get {
            if (player == null) 
                return -1;
            return player.position.x - initialPosition;
        }
    }
    int distanceTravelled;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        initialPosition = player.position.x;
    }

    private void Update() {
        SetText();
    }

    void SetText() {
        if (currentPosition > distanceTravelled) {
            distanceTravelled = (int)currentPosition;
            text.text = distanceTravelled.ToString();
        }
    }
}