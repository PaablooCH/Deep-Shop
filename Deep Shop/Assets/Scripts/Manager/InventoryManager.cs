using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;

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

    public delegate void OnModifyQuantity(int idModifiedItem, int amount);
    public OnModifyQuantity onModifyQuantity;

    public delegate void OnRemoveItem(int idRemovedItem);
    public OnRemoveItem onRemoveItem;
    #endregion

    private Dictionary<int, int> inventory = new(); // key -> product id, value -> ammount

    void Start()
    {
        // Start items
        
    }

    public void StartItems()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("startInventory");
        if (jsonAsset != null)
        {
            StartInventoryArray dataPrefab = JsonUtility.FromJson<StartInventoryArray>(jsonAsset.text);
            foreach (StartInventoryInfo startInventory in dataPrefab.startInventory)
            {
                ModifyInventory(startInventory.productID, startInventory.amount);
            }
        }
    }

    public int GetInventory(int id)
    {
        return inventory[id];
    }

    // n can be negative (substract) or positive (sum)
    public void ModifyInventory(int id, int n)
    {
        if (!inventory.ContainsKey(id) && onAddItem != null)
        {
            onAddItem(ProductsManager.instance.SearchProductByID(id));
            inventory[id] = 0;
        }
        if (inventory[id] + n < 0)
        {
            Debug.LogError("Attempt to leave a negative value in the inventory");
            return;
        }
        inventory[id] += n;
        if (inventory[id] > 0 && onModifyQuantity != null)
        {
            onModifyQuantity(id, inventory[id]);
        }
        else if (inventory[id] == 0 && onRemoveItem != null)
        {
            onRemoveItem(id);
        }
    }
}
