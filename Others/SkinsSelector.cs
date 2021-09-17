using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SkinsSelector : MonoBehaviour {
    [SerializeField] SkinsHolder skinsHolder;

    [Space]
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button selectButton;
    [SerializeField] DitzeGames.MobileJoystick.TouchField rotatorTouchfield;
    [SerializeField] GameObject cam;

    [Header("Descriptions")]
    [SerializeField] Image maxHealthImage;
    [SerializeField] Image speedImage;
    [SerializeField] TextMeshProUGUI priceText;

    [Header("Configuration")]
    [SerializeField] float highestHealth;
    [SerializeField] float highestSpeed;

    [Header("Stars")]
    [SerializeField] TextMeshProUGUI AvailableStarsText;
    [SerializeField] TextMeshProUGUI TotalStarsText;

    [Header("Sounds")]
    [SerializeField] Sound purchaseSound;
    [SerializeField] Sound notEnoughPointsSound;
    [SerializeField] Sound selectSound;

    Transform player;

    int currentlyShownIndex;
    Vector3 playerInitialRotation;

    private void Start() {
        player = FindObjectOfType<Player>().transform;
        playerInitialRotation = player.transform.eulerAngles;

        rightArrow.onClick.AddListener(SelectNextSkin);
        leftArrow.onClick.AddListener(SelectPreviousSkin);

        selectButton.onClick.AddListener(SelectCurrentShownSkin);
        purchaseButton.onClick.AddListener(TryPurchasing);

        currentlyShownIndex = PlayerStats.Instance.SkinIndex;
        UpdateStarsText();
        UpdateUI();
    }

    private void Update() {
        if (rotatorTouchfield.Pressed) {
            player.localEulerAngles = player.localEulerAngles + new Vector3(0, -1 * rotatorTouchfield.TouchDist.x, 0);
        }
    }
    
    void SelectNextSkin() {
        SelectSkin( (currentlyShownIndex + 1) % skinsHolder.Skins.Length );
    }

    void SelectPreviousSkin() {
        SelectSkin( currentlyShownIndex - 1 < 0 ? skinsHolder.Skins.Length - 1 : currentlyShownIndex - 1 );
    }

    void SelectSkin(int index) {
        PlayerSkin newSkin = skinsHolder.Skins[index];
        PlayerSkin currentSkin = skinsHolder.Skins[currentlyShownIndex];
        
        currentSkin.gameObject.SetActive(false);
        newSkin.gameObject.SetActive(true);
        
        currentlyShownIndex = index;
        UpdateUI();
    }

    void UpdateUI() {
        PlayerSkin skin = skinsHolder.Skins[currentlyShownIndex];

        maxHealthImage.DOFillAmount( Mathf.Max(PlayerStats.Instance.MaxHealth, skin.MaxHealth) / highestHealth, .25f );

        speedImage.DOFillAmount( Mathf.Max(PlayerStats.Instance.Speed, skin.Speed) / highestSpeed, .25f );

        priceText.text = skin.Price.ToString() + " stars";

        selectButton.gameObject.SetActive(skin.Purchased && PlayerStats.Instance.SkinIndex != currentlyShownIndex);
        purchaseButton.gameObject.SetActive(!skin.Purchased);
        priceText.gameObject.SetActive(!skin.Purchased);
    }

    private void OnEnable() {
        cam?.SetActive(true);
        currentlyShownIndex = PlayerStats.Instance.SkinIndex;
    }

    private void OnDisable() {
        player?.DORotate(playerInitialRotation, 1f);
        cam?.SetActive(false);  
        SelectSkin(PlayerStats.Instance.SkinIndex);
    }

    void SelectCurrentShownSkin() {
        PlayerStats.Instance.SetSkinIndex(currentlyShownIndex);
        selectButton.gameObject.SetActive(false);

        AudioManager.Instance.Play(selectSound.Audio, selectSound.Volume);
    }

    void UpdateStarsText() {
        TotalStarsText.text = PlayerStats.Instance.StarPoints.ToString();
        AvailableStarsText.text = PlayerStats.Instance.AvailableStarPoints.ToString();
    }

    void TryPurchasing() {
        bool success = PlayerStats.Instance.StarPoints - skinsHolder.Skins[currentlyShownIndex].Price >= 0;
        if (success) {
            Purchase();
        } else {
            AudioManager.Instance.Play(notEnoughPointsSound.Audio, notEnoughPointsSound.Volume);
        }
    }

    void Purchase() {
        AudioManager.Instance.Play(purchaseSound.Audio, purchaseSound.Volume);
        
        PlayerSkin skin = skinsHolder.Skins[currentlyShownIndex];
        skin.Purchase();
        PlayerStats.Instance.UseStar(skin.Price);

        if (skin.MaxHealth > PlayerStats.Instance.MaxHealth)
            PlayerStats.Instance.SetMaxHealth(skin.MaxHealth);
        
        if (skin.Speed > PlayerStats.Instance.Speed)
            PlayerStats.Instance.SetSpeed(skin.Speed);

        SelectCurrentShownSkin();
        UpdateStarsText();
        UpdateUI();

        FindObjectOfType<SaveManager>().Save();
    }
}