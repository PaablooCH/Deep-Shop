using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeUI : MonoBehaviour, IUIProduct, IUIConfirmation, IUIReject
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
    }

    public void OpenUI(GameObject product)
    {
        if (product.TryGetComponent(out ProductInfo component))
        {
            _actualProduct = component;
            CanvasManager.instance.ActiveUI(gameObject);
            SpriteRenderer spriteProduct = _actualProduct.GetComponent<SpriteRenderer>();

            // Update Product Color
            Image image = transform.Find("Product Image").gameObject.GetComponent<Image>();
            image.sprite = spriteProduct.sprite;
            image.color = spriteProduct.color;

            // Slider initial value
            Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
            tradeUISlider.value = _actualProduct.Product.buyPrice;

            TextMeshProUGUI text = _sliderAndText.transform.Find("Slider Info").gameObject.GetComponent<TextMeshProUGUI>();
            text.text = _actualProduct.Product.buyPrice.ToString("0.0") + " G";

            // InputField init
            int inventory = InventoryManager.instance.GetInventory(_actualProduct.Product.id);
            TMP_InputField tMP_InputField = _inputField.GetComponent<TMP_InputField>();
            tMP_InputField.text = inventory > 0 ? "1" : "0";
            InputNumberInteraction inputNumberInteraction = _inputField.GetComponent<InputNumberInteraction>();
            inputNumberInteraction.UpperLimit = inventory;
            inputNumberInteraction.LowerLimit = 0;
        }
    }

    public void Exit()
    {
        CanvasManager.instance.FreeUI();
        _actualProduct = null;
    }

    public void Confirm()
    {
        TMP_InputField tMP_InputField = _inputField.GetComponent<TMP_InputField>();
        Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PlayerStats.instance.Trade(_actualProduct, int.Parse(tMP_InputField.text), tradeUISlider.value);
        _actualProduct = null;
        _sellInteraction.EndInteraction();
        CanvasManager.instance.FreeUI();
    }

    public void Reject()
    {
        PlayerStats.instance.Karma -= _actualProduct.Product.karma;
        _sellInteraction.EndInteraction();
        CanvasManager.instance.FreeUI();
    }
}
