using UnityEngine;

public class ManageInventoryGrid : ManageProductsInGrid
{
    private void Start()
    {
        GameEventsManager.instance.inventoryEvent.onAddItem += AddItem;
        GameEventsManager.instance.inventoryEvent.onModifyQuantity += ModifyQuantity;
        GameEventsManager.instance.inventoryEvent.onRemoveItem += RemoveItem;
        WaitInventory.instance.InventorySlotReady = true;
    }

    public override GameObject AddItem(string newItem)
    {
        int id = int.Parse(newItem);
        GameObject slot = base.AddItem(newItem);
        Product product = ProductsManager.instance.GetProductInfo(id).Product;
        TooltipTrigger tooltipTrigger = slot.GetComponent<TooltipTrigger>();
        tooltipTrigger.Header = product.productName;
        tooltipTrigger.Body = product.description;
        return slot;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inventoryEvent.onAddItem -= AddItem;
        GameEventsManager.instance.inventoryEvent.onModifyQuantity -= ModifyQuantity;
        GameEventsManager.instance.inventoryEvent.onRemoveItem -= RemoveItem;
    }
}
