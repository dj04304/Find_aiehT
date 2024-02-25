using UnityEngine;
using UnityEngine.UI;

public class ShopPotion : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    [SerializeField] private PlayerSO _playerData;
    public ShopPotionInfo ShopPotionInfo;
    public ShopPotionInfoPopup ShopPotionInfoPopup;
    public Button ShopPopupButton;

    public Image PotionImage;

    private Button _slotButton;


    public void Init(PotionSO data)
    {
        PotionSO = data;
        PotionImage.sprite = data.sprite;
    }

    public void SetItemInfo()
    {
        _slotButton = GetComponent<Button>();
        _slotButton.onClick.RemoveAllListeners();

        _slotButton.onClick.AddListener(() => 
        {
            ShopPotionInfo.ShowItemInfo(PotionSO);
            OnShopPopupButton();

        });
    }

    private void OnShopPopupButton()
    {
        ShopPopupButton.onClick.RemoveAllListeners();

        ShopPopupButton.onClick.AddListener(() =>
        {
            if (_playerData.PlayerData.PlayerGold >= PotionSO.Price)
            {
               
                ShopPotionInfoPopup.ShowPopup(PotionSO);

                ShopPotionInfoPopup.gameObject.SetActive(true);
            }
        });
    }
}
