public class InventorySlot : ManageProductsInGrid
{
    private void Start()
    {
        InventoryManager.instance.onAddItem += AddItem;
        InventoryManager.instance.onModifyQuantity += ModifyQuantity;
        InventoryManager.instance.onRemoveItem += RemoveItem;
        InventoryManager.instance.StartItems();
    }
}
