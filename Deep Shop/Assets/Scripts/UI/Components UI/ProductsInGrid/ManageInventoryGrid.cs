using UnityEngine;

public class ManageInventoryGrid : ManageSlotsInGrid
{
    private void Start()
    {
        InventoryManager.instance.onAddItem += AddItem;
        InventoryManager.instance.onModifyQuantity += ModifyQuantity;
        InventoryManager.instance.onRemoveItem += RemoveItem;
        WaitInventory.instance.InventorySlotReady = true;
    }

    public override GameObject AddItem(GameObject newItem)
    {
        GameObject slot = base.AddItem(newItem);
        Product product = newItem.GetComponent<ProductInfo>().Product;
        TooltipTrigger tooltipTrigger = slot.GetComponent<TooltipTrigger>();
        tooltipTrigger.Header = product.productName;
        tooltipTrigger.Body = product.description;
        return slot;
    }
}
