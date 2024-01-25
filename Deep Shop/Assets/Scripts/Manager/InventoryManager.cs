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
    public delegate GameObject OnAddItem(GameObject newItem);
    public OnAddItem onAddItem;

    public delegate void OnModifyQuantity(int idModifiedItem, int amount);
    public OnModifyQuantity onModifyQuantity;

    public delegate void OnRemoveItem(int idRemovedItem);
    public OnRemoveItem onRemoveItem;
    #endregion

    private Dictionary<int, int> _inventory = new(); // key -> product id, value -> ammount
    
    private const string START_INVENTORY_PATH = "JSONs/startInventory";

    public IEnumerator StartItemsAsync()
    {
        ResourceRequest request = Resources.LoadAsync<TextAsset>(START_INVENTORY_PATH);

        while (!request.isDone)
        {
            float progress = request.progress;
            Debug.Log("Inventory load progress: " + progress * 100f + "%");
            yield return null;
        }
        Debug.Log("Inventory load progress: 100%");

        TextAsset jsonAsset = request.asset as TextAsset;

        if (jsonAsset != null)
        {
            StartInventoryArray dataPrefab = JsonUtility.FromJson<StartInventoryArray>(jsonAsset.text);
            foreach (StartInventoryInfo startInventory in dataPrefab.startInventory)
            {
                ModifyInventory(startInventory.productID, startInventory.amount);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _inventory[0]++;
        }
    }

    public int GetInventory(int id)
    {
        return _inventory[id];
    }

    // n can be negative (substract) or positive (sum)
    public void ModifyInventory(int id, int n)
    {
        if (!_inventory.ContainsKey(id))
        {
            _inventory[id] = 0;
            onAddItem?.Invoke(ProductsManager.instance.SearchProductByID(id));
        }
        if (_inventory[id] + n < 0)
        {
            Debug.LogError("Attempt to leave a negative value in the inventory");
            return;
        }
        _inventory[id] += n;
        if (_inventory[id] > 0 && onModifyQuantity != null)
        {
            onModifyQuantity(id, _inventory[id]);
        }
        else if (_inventory[id] == 0 && onRemoveItem != null)
        {
            onRemoveItem(id);
        }
    }
}
