using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tradeUI;
    [SerializeField]
    private GameObject sliderAndText;
    [SerializeField]
    private GameObject inputText;
    [SerializeField]
    private PauseManager pauseManager;
    [SerializeField]
    private PlayerStats playerStats;

    public void OpenTrade(GameObject product)
    {
        if (tradeUI != null)
        {
            pauseManager.Pause();
            SpriteRenderer spriteProduct = product.GetComponent<SpriteRenderer>();
            Product productInfo = product.GetComponent<Product>();

            tradeUI.SetActive(true);

            // Update Product Color
            RawImage rawImage = tradeUI.transform.Find("Product Image").gameObject.GetComponent<RawImage>();
            rawImage.color = spriteProduct.color;

            // Slider initial value
            Slider tradeUISlider = sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
            tradeUISlider.value = productInfo.BuyPrice;

            TextMeshProUGUI text = sliderAndText.transform.Find("Slider Info").gameObject.GetComponent<TextMeshProUGUI>();
            text.text = productInfo.BuyPrice.ToString("0.0") + " G";

            // InputField init
            int inventory = playerStats.GetInventory(productInfo);
            TMP_InputField tMP_InputField = inputText.GetComponent<TMP_InputField>();
            tMP_InputField.text = inventory > 0 ? "1" : "0";
            inputText.GetComponent<InputNumberInteraction>().AmountProduct = inventory;
        }
    }

    public void Exit()
    { 
        if (tradeUI != null)
        {
            pauseManager.Restart();
            tradeUI.SetActive(false);
        }
    }

    public void Confirm(float price)
    {

    }
}
