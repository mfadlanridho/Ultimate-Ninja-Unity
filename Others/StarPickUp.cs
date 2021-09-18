using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using SegmentPoolingSystem;

public class StarPickUp : MonoBehaviour {
    [SerializeField] Sound pickupSound;
    [SerializeField] List<Transform> stars;
    [SerializeField] float speed = 100f;

    Collider[] player = new Collider[1];

    TextMeshProUGUI textUI;
    float time;

    private void Start() {
        TrapFloor trapFloor = GetComponentInParent<TrapFloor>();
        trapFloor.OnThisFloorCallback += StartCountdown;

        // textUI = GameObject.FindWithTag("Star Collect").GetComponent<TextMeshProUGUI>();
        // Debug.Log("text ui is " + textUI);
    }

    private void Update() {
        for (int i = 0; i < stars.Count; i++) {
            Transform star = stars[i];
            Rotate(star);

            if (time > 0) {
                bool collided = DetectPlayerCollision(star);
                if (collided)
                    break;
            }
        }

        if (time > 0) {
            time -= Time.deltaTime;
            UpdateText();
        }
        else if (activated) {
            SetTextUIActive(false);
            Destroy(gameObject);
        }
    }

    bool DetectPlayerCollision(Transform star) {                
        player[0] = null;
        Physics.OverlapSphereNonAlloc(star.position, 1f, player, LayerMask.GetMask("Player"));
        if (player[0] != null) {
            AudioManager.Instance.Play(pickupSound.Audio, pickupSound.Volume, star.position);
            GameManager.Instance.PickUpStar();
            
            stars.Remove(star);
            Destroy(star.gameObject);
            return true;
        }
        return false;
    }

    void Rotate(Transform star) {                   
        star.Rotate(0, 0, Time.deltaTime * speed);
    }

    bool demoMode = true;
    void StartCountdown() {
        if (demoMode) {
            demoMode = false;
            return;
        }

        time = 20;
        Activate();
    }

    bool activated;
    void Activate() {
        activated = true;
        SetTextUIActive(true);

        transform.DOMoveY(0f, 1f);
        AnimateText();
        UpdateText();
    }

    void UpdateText() {
        textUI.text = "Collect the stars in " + Mathf.Ceil(time).ToString();
    }

    void AnimateText() {
        Sequence s = DOTween.Sequence();
        s.Append(textUI.fontSharedMaterial.DOFloat(1f, ShaderUtilities.ID_GlowOuter, 1f));
        s.Append(textUI.fontSharedMaterial.DOFloat(.1f, ShaderUtilities.ID_GlowOuter, 1f));
        s.SetLoops(-1);
    }

    public void SetTextUI(TextMeshProUGUI textUI) {
        this.textUI = textUI;
        Debug.Log("star collect is " + this.textUI);       
    }

    void SetTextUIActive(bool active) {
        textUI.gameObject.SetActive(active);
    }
}