using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    #region Singleton
    public static ItemsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Product Manager singleton already exists.");
            return;
        }
        instance = this;
        _itemMap = CreateItemMap();
    }
    #endregion

    private Dictionary<string, Item> _itemMap;

    private int _totalWeightSpawn = 0;

    public Item GetItemByID(string id)
    {
        // Search by item id
        foreach (Item item in _itemMap.Values)
        {
            if (item.GetItemId() == id)
            {
                return item;
            }
        }

        // If not found, display error and return null
        Debug.LogWarning("Doesn`t exist any Item with id: " + id + ".");
        return null;
    }

    public bool ExistsItem(string id)
    {
        return GetItemByID(id) != null;
    }

    public Item GetItemByName(string name)
    {
        // Search by item name
        foreach (Item item in _itemMap.Values)
        {
            if (item.ItemInfo.NameItem == name)
            {
                return item;
            }
        }

        // If not found, display error and return null
        Debug.LogWarning("Doesn`t exist any Item with name: " + name + ".");
        return null;
    }

    public string RandomItemID()
    {
        return RandomItem().GetItemId();
    }

    public Item RandomItem()
    {
        int randomWeight = Random.Range(1, _totalWeightSpawn + 1);

        int actualWeight = 0;
        Item selected = null;
        foreach (Item item in _itemMap.Values)
        {
            actualWeight += item.ItemInfo.WeightSpawn;
            if (randomWeight <= actualWeight)
            {
                selected = item;
                break;
            }
        }

        return selected;
    }

    public int HowManyItemsExist()
    {
        return _itemMap.Count;
    }

    private Dictionary<string, Item> CreateItemMap()
    {
        // Get all the Scriptable Objects from the quests folder
        ItemInfoSO[] allItems = Resources.LoadAll<ItemInfoSO>("Items");

        // Insert all the quests into a dictionary
        Dictionary<string, Item> auxDictionary = new();
        foreach (ItemInfoSO info in allItems)
        {
            if (auxDictionary.ContainsKey(info.IdItem))
            {
                Debug.LogWarning("The IdItem: " + info.IdItem + " has been found repeated, while creating the Item Map.");
                continue;
            }
            _totalWeightSpawn += info.WeightSpawn;
            auxDictionary.Add(info.IdItem, new Item(info));
        }

        //return the dictionary
        return auxDictionary;
    }
}
