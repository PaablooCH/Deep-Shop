using TMPro;

public class InventorySlot : ManageProductsInGrid
{
    private void Start()
    {
        InventoryManager.instance.onAddItem += AddItem;
        InventoryManager.instance.onModifyQuantity += ModifyQuantity;
        InventoryManager.instance.onRemoveItem += RemoveItem;
        InventoryManager.instance.StartItems();
    }

    private void ModifyQuantity(ProductType modifiedItem, int amount)
    {
        int index = productsInGrid.FindIndex((productType) => productType == modifiedItem);
        TextMeshProUGUI text = gridTransform.GetChild(index).GetComponentInChildren<TextMeshProUGUI>();
        text.text = amount.ToString();
    }
}
