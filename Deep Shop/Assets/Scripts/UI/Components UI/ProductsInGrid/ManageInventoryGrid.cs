public class ManageInventoryGrid : ManageProductsInGrid
{
    private void Start()
    {
        InventoryManager.instance.onAddItem += AddItem;
        InventoryManager.instance.onModifyQuantity += ModifyQuantity;
        InventoryManager.instance.onRemoveItem += RemoveItem;
        WaitInventory.instance.InventorySlotReady = true;
    }
}
