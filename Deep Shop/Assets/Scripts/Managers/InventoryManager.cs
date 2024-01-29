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
            Debug.LogError("Inventory Manager singleton already exists.");
            return;
        }
        instance = this;
    }
    #endregion

    [Min(0f)]
    [SerializeField] private float _money = 100f;

    [Range(-100f, 100f)]
    [SerializeField] private float _karma = 0f;

    public float Karma
    {
        get => _karma;
        set
        {
            GameEventsManager.instance.inventoryEvent.KarmaChanged(value);
            _karma = value;
        }
    }

    public float Money
    {
        get => _money;
        set
        {
            GameEventsManager.instance.inventoryEvent.MoneyChange(value);
            _money = value;
        }
    }

    private Dictionary<string, int> _inventory = new(); // key -> item id, value -> ammount
    
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
                ModifyInventory(startInventory.idItem, startInventory.amount);
            }
        }
    }

    public void Trade(Item item, int quantity, float price)
    {
        ModifyInventory(item.GetItemId(), -quantity);
        Karma += item.CalculateKarma(price);
        if (item.CalculatePercentatgeBuy(price) < 2.5f)
        {
            Money += price;
        }
    }

    public int GetInventory(string id)
    {
        return _inventory[id];
    }

    // n can be negative (substract) or positive (sum)
    public void ModifyInventory(string id, int n)
    {
        if (!ItemsManager.instance.ExistsItem(id))
        {
            Debug.LogWarning("Product with id: " + id + " doesn't exist.");
            return;
        }
        if (!_inventory.ContainsKey(id))
        {
            _inventory[id] = 0;
            GameEventsManager.instance.inventoryEvent.AddItem(id);
        }
        if (_inventory[id] + n < 0)
        {
            Debug.LogError("Attempt to leave a negative value in the inventory");
            return;
        }
        _inventory[id] += n;
        if (_inventory[id] > 0)
        {
            GameEventsManager.instance.inventoryEvent.ModifyQuantity(id, _inventory[id]);
        }
        else if (_inventory[id] == 0)
        {
            GameEventsManager.instance.inventoryEvent.RemoveItem(id);
        }
    }
}
