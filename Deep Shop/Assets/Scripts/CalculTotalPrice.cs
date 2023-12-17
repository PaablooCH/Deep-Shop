using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculTotalPrice : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_InputField inputText;
    [SerializeField]
    private TextMeshProUGUI priceText;

    // Update is called once per frame
    void Update()
    {
        float price = slider.value * float.Parse(inputText.text);
        priceText.text = price.ToString("0.0") + " G";
    }
}
