using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculateTotalPrice : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeField]
    private TextMeshProUGUI _priceText;

    // Update is called once per frame
    void Update()
    {
        if (float.TryParse(_inputField.text, out float inputValue))
        {
            float price = _slider.value * inputValue;
            _priceText.text = price.ToString("0.0") + " G";
        }
        else
        {
            _priceText.text = "";
            Debug.LogWarning("Wrong value in input field");
        }
    }
}
