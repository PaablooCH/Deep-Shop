using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    #region Listeners
    public delegate void OnAddItem(GameObject newItem);
    public OnAddItem onAddItem;

    public delegate void OnModifyItem(ProductType modifiedItem, int amount);
    public OnModifyItem onModifyItem;

    public delegate void OnRemoveItem(ProductType removedItem);
    public OnRemoveItem onRemoveItem;
    #endregion

    private Dictionary<ProductType, int> inventory = new();

    void Start()
    {
        // Start items
        
    }

    public void StartItems()
    {
        ModifyInventory(ProductType.LEGAL_1, 5);
        ModifyInventory(ProductType.LEGAL_2, 5);
        ModifyInventory(ProductType.LEGAL_3, 2);
        ModifyInventory(ProductType.NOT_LEGAL_1, 3);
        ModifyInventory(ProductType.NOT_LEGAL_2, 1);
        ModifyInventory(ProductType.NOT_LEGAL_3, 1);
    }

    public int GetInventory(ProductType productType)
    {
        return inventory[productType];
    }

    // n can be negative (substract) or positive (sum)
    public void ModifyInventory(ProductType type, int n)
    {
        if (!inventory.ContainsKey(type) && onAddItem != null)
        {
            onAddItem(ProductsManager.instance.SearchProduct(type));
            inventory[type] = 0;
        }
        if (inventory[type] + n < 0)
        {
            Debug.LogError("Attempt to leave a negative value in the inventory");
            return;
        }
        inventory[type] += n;
        if (inventory[type] > 0 && onModifyItem != null)
        {
            onModifyItem(type, n);
        }
        else if (inventory[type] == 0 && onRemoveItem != null)
        {
            onRemoveItem(type);
        }
    }
}
