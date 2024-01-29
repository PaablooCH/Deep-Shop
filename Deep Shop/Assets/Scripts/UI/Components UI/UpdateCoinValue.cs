using UnityEngine;
using TMPro;

public class UpdateCoinValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _quantity;

    // Start is called before the first frame update
    void Start()
    {
        GameEventsManager.instance.inventoryEvent.onMoneyChanged += UptadeQuantity;
        _quantity.text = InventoryManager.instance.Money.ToString("0.00");
    }

    private void UptadeQuantity(float newQuantity)
    {
        _quantity.text = newQuantity.ToString("0.00");
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inventoryEvent.onMoneyChanged -= UptadeQuantity;
    }
}
