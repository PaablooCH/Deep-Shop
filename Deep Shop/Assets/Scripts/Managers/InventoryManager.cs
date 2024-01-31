using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
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

    private float _money;

    private float _karma;

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
            Debug.LogWarning("Attempt to leave a negative value in the inventory");
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

    public void SaveData(ref GameData data)
    {
        InventoryData inventoryData = new InventoryData();
        inventoryData.karma = _karma;
        inventoryData.moneyCount = _money;
        inventoryData.items = _inventory;

        data.inventoryData = inventoryData;
    }

    public void LoadData(GameData data)
    {
        Money = data.inventoryData.moneyCount;
        Karma = data.inventoryData.karma;

        foreach (KeyValuePair<string, int> item in data.inventoryData.items)
        {
            ModifyInventory(item.Key, item.Value);
        }
    }
}
