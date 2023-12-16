using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenTradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject tradeUI;

    public void OpenTrade(GameObject product)
    {
        if (tradeUI != null)
        {
            SpriteRenderer spriteProduct = product.GetComponent<SpriteRenderer>();
            Product productInfo = product.GetComponent<Product>();

            tradeUI.SetActive(true);

            // Update Product Color
            RawImage rawImage = tradeUI.transform.Find("Product Image").gameObject.GetComponent<RawImage>();
            rawImage.color = spriteProduct.color;

            // Slider initial value
            Slider tradeUISlider = tradeUI.transform.Find("Product Slider").gameObject.GetComponent<Slider>();
            tradeUISlider.value = productInfo.BuyPrice;

            TextMeshProUGUI text = tradeUI.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
            text.text = productInfo.BuyPrice.ToString("0.0") + " G";
        }
    }
}
