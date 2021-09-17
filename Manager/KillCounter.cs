using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    EnemyDetector detector;
    int killCount = -1;

    private void Start() {
        detector = FindObjectOfType<EnemyDetector>();
        detector.OnAfterRemoveEnemy += SetText;
        SetText();
    }

    void SetText() {
        killCount++;
        text.text = killCount.ToString();
    }
}