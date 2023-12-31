using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private GameObject _sliderAndText;
    [SerializeField]
    private GameObject _inputField;

    [SerializeField]
    private SellInteraction _sellInteraction;

    private ProductInfo _actualProduct;

    private void Start()
    {
        gameObject.SetActive(false);
        UIManager.instance.AddUI(UIType.TRADE, gameObject);
    }

    public void OpenTrade(GameObject product)
    {
        UIManager.instance.ActiveTradeUI();
        SpriteRenderer spriteProduct = product.GetComponent<SpriteRenderer>();
        ProductInfo productInfo = product.GetComponent<ProductInfo>();
        _actualProduct = productInfo;

        // Update Product Color
        Image image = transform.Find("Product Image").gameObject.GetComponent<Image>();
        image.sprite = spriteProduct.sprite;
        image.color = spriteProduct.color;

        // Slider initial value
        Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
        tradeUISlider.value = productInfo.Product.buyPrice;

        TextMeshProUGUI text = _sliderAndText.transform.Find("Slider Info").gameObject.GetComponent<TextMeshProUGUI>();
        text.text = productInfo.Product.buyPrice.ToString("0.0") + " G";

        // InputField init
        int inventory = InventoryManager.instance.GetInventory(productInfo.Product.id);
        TMP_InputField tMP_InputField = _inputField.GetComponent<TMP_InputField>();
        tMP_InputField.text = inventory > 0 ? "1" : "0";
        _inputField.GetComponent<InputNumberInteraction>().AmountProduct = inventory;
    }

    public void Exit()
    { 
        UIManager.instance.FreeUI(UIType.TRADE);
        _actualProduct = null;
    }

    public void Confirm()
    {
        TMP_InputField tMP_InputField = _inputField.GetComponent<TMP_InputField>();
        Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PlayerStats.instance.Trade(_actualProduct, int.Parse(tMP_InputField.text), tradeUISlider.value);
        _actualProduct = null;
        _sellInteraction.EndTrade();
        UIManager.instance.FreeUI(UIType.TRADE);
    }

    public void Reject()
    {
        PlayerStats.instance.Karma -= _actualProduct.Product.karma;
        _sellInteraction.EndTrade();
        UIManager.instance.FreeUI(UIType.TRADE);
    }
}
