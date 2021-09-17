using UnityEngine;
using UnityEngine.UI;

public class GameplayButtons : MonoBehaviour {
    [SerializeField] Button basicAttackButton;
    [SerializeField] Button comboAttackButon;
    [SerializeField] Button rollButton;

    private void Start() {
        GameObject player = GameObject.FindWithTag("Player");

#if UNITY_EDITOR
        Debug.Log("Assigning Buttons");
#endif
        
        basicAttackButton.onClick.AddListener(player.GetComponent<BasicAttack>().InitiateAttack);
        comboAttackButon.onClick.AddListener(player.GetComponent<ComboAttack>().InitiateAttack);
        rollButton.onClick.AddListener(player.GetComponent<Roll>().DoRoll);
    }

    // private void OnDisable() {
    //     GameObject player = GameObject.FindWithTag("Player");

    //     basicAttackButton.onClick.RemoveListener(player.GetComponent<BasicAttack>().InitiateAttack);
    //     comboAttackButon.onClick.RemoveListener(player.GetComponent<ComboAttack>().InitiateAttack);
    //     rollButton.onClick.RemoveListener(player.GetComponent<Roll>().DoRoll);
    // }
}