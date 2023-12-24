using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private GameObject tradeUI;
    [SerializeField]
    private GameObject sliderAndText;
    [SerializeField]
    private GameObject inputText;

    [SerializeField]
    private SellInteraction sellInteraction;

    private ProductInfo actualProduct;

    private void Start()
    {
        if (tradeUI != null)
        {
            tradeUI.SetActive(false);
            UIManager.instance.AddUI(UIType.TRADE, tradeUI);
        }
    }

    public void OpenTrade(GameObject product)
    {
        if (tradeUI != null)
        {
            UIManager.instance.ActiveTradeUI();
            SpriteRenderer spriteProduct = product.GetComponent<SpriteRenderer>();
            ProductInfo productInfo = product.GetComponent<ProductInfo>();
            actualProduct = productInfo;

            // Update Product Color
            Image image = tradeUI.transform.Find("Product Image").gameObject.GetComponent<Image>();
            image.sprite = spriteProduct.sprite;
            image.color = spriteProduct.color;

            // Slider initial value
            Slider tradeUISlider = sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
            tradeUISlider.value = productInfo.Product.buyPrice;

            TextMeshProUGUI text = sliderAndText.transform.Find("Slider Info").gameObject.GetComponent<TextMeshProUGUI>();
            text.text = productInfo.Product.buyPrice.ToString("0.0") + " G";

            // InputField init
            int inventory = InventoryManager.instance.GetInventory(productInfo.Product.productType);
            TMP_InputField tMP_InputField = inputText.GetComponent<TMP_InputField>();
            tMP_InputField.text = inventory > 0 ? "1" : "0";
            inputText.GetComponent<InputNumberInteraction>().AmountProduct = inventory;
        }
    }

    public void Exit()
    { 
        if (tradeUI != null)
        {
            UIManager.instance.FreeUI(UIType.TRADE);
            actualProduct = null;
        }
    }

    public void Confirm()
    {
        TMP_InputField tMP_InputField = inputText.GetComponent<TMP_InputField>();
        Slider tradeUISlider = sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PlayerStats.instance.Trade(actualProduct, int.Parse(tMP_InputField.text), tradeUISlider.value);
        actualProduct = null;
        sellInteraction.EndTrade();
        UIManager.instance.FreeUI(UIType.TRADE);
    }

    public void Reject()
    {
        PlayerStats.instance.Karma -= actualProduct.Product.karma;
        sellInteraction.EndTrade();
        UIManager.instance.FreeUI(UIType.TRADE);
    }
}
