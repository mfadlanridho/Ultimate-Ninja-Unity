using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreeSkinButton : MonoBehaviour {
    [SerializeField] GameObject easterEggObject;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject succeededText;

    public void CheckInput() {
        if (inputField.text == "notorious") {
            succeededText.gameObject.SetActive(true);
            FindObjectOfType<SkinsHolder>().Skins[4].Purchase();
        }
    }

    public void TryActivateEasterEgg() {
        if (PlayerStats.Instance.UnlockedLevelCount >= 10) {
            easterEggObject.SetActive(true);
        }
    }
}