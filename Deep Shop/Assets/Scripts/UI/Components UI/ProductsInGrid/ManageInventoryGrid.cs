public class ManageInventoryGrid : ManageSlotsInGrid
{
    private void Start()
    {
        InventoryManager.instance.onAddItem += AddItem;
        InventoryManager.instance.onModifyQuantity += ModifyQuantity;
        InventoryManager.instance.onRemoveItem += RemoveItem;
        WaitInventory.instance.InventorySlotReady = true;
    }
}
