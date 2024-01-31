using UnityEngine;

public class ManageInventoryGrid : ManageItemsInGrid
{
    private void Start()
    {
        GameEventsManager.instance.inventoryEvent.onAddItem += AddItem;
        GameEventsManager.instance.inventoryEvent.onModifyQuantity += ModifyQuantity;
        GameEventsManager.instance.inventoryEvent.onRemoveItem += RemoveItem;
    }

    public override GameObject AddItem(string newItem)
    {
        GameObject slot = base.AddItem(newItem);
        Item item = ItemsManager.instance.GetItemByID(newItem);
        TooltipTrigger tooltipTrigger = slot.GetComponent<TooltipTrigger>();
        tooltipTrigger.Header = item.ItemInfo.NameItem;
        tooltipTrigger.Body = item.ItemInfo.Description;
        return slot;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inventoryEvent.onAddItem -= AddItem;
        GameEventsManager.instance.inventoryEvent.onModifyQuantity -= ModifyQuantity;
        GameEventsManager.instance.inventoryEvent.onRemoveItem -= RemoveItem;
    }
}
