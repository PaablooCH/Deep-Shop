using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeUI : MonoBehaviour, IUIItem, IUIConfirmation, IUIReject
{
    // UI elements
    [SerializeField] private GameObject _sliderAndText;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private SellInteraction _sellInteraction;

    private Item _actualItem;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(Item item)
    {
            _actualItem = item;
            UIManager.instance.ActiveUI(UIs.TRADE);

            // Update Product Color
            Image image = transform.Find("Product Image").gameObject.GetComponent<Image>();
            image.sprite = _actualItem.ItemInfo.Sprite;
            image.color = _actualItem.ItemInfo.Color;

            // Slider initial value
            Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
            tradeUISlider.value = _actualItem.ItemInfo.BuyPrice;

            TextMeshProUGUI text = _sliderAndText.transform.Find("Slider Info").gameObject.GetComponent<TextMeshProUGUI>();
            text.text = _actualItem.ItemInfo.BuyPrice.ToString("0.0") + " G";

            // InputField init
            int inventory = PlayerManager.instance.GetPlayerInventory().GetInventory(_actualItem.GetItemId());
            _inputField.text = inventory > 0 ? "1" : "0";
            InputNumberInteraction inputNumberInteraction = _inputField.GetComponent<InputNumberInteraction>();
            inputNumberInteraction.UpperLimit = inventory;
            inputNumberInteraction.LowerLimit = 0;
    }

    public void Exit()
    {
        UIManager.instance.FreeUI();
        _actualItem = null;
    }

    public void Confirm()
    {
        Slider tradeUISlider = _sliderAndText.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PlayerManager.instance.GetPlayerInventory().Trade(_actualItem, int.Parse(_inputField.text), tradeUISlider.value);
        _actualItem = null;
        _sellInteraction.EndInteraction();
        UIManager.instance.FreeUI();
    }

    public void Reject()
    {
        PlayerManager.instance.GetPlayerInventory().Karma -= _actualItem.ItemInfo.Karma;
        _sellInteraction.EndInteraction();
        UIManager.instance.FreeUI();
    }
}
