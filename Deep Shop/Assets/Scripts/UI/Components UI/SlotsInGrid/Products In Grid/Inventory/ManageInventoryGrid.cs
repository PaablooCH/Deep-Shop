using UnityEngine;

public class ManageInventoryGrid : ManageItemsInGrid
{
    private void Start()
    {
        GameEventsMediator.instance.inventoryEvent.onAddItem += AddItem;
        GameEventsMediator.instance.inventoryEvent.onModifyQuantity += ModifyQuantity;
        GameEventsMediator.instance.inventoryEvent.onRemoveItem += RemoveItem;
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
        GameEventsMediator.instance.inventoryEvent.onAddItem -= AddItem;
        GameEventsMediator.instance.inventoryEvent.onModifyQuantity -= ModifyQuantity;
        GameEventsMediator.instance.inventoryEvent.onRemoveItem -= RemoveItem;
    }
}
