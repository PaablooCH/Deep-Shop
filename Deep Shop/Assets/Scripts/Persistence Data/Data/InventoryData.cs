using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public float moneyCount;
    public float karma;
    public Dictionary<string, int> items = new();

    public void StartItemsJSON()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("JSONs/startInventory");

        if (jsonAsset != null)
        {
            StartInventoryInfo dataPrefab = JsonUtility.FromJson<StartInventoryInfo>(jsonAsset.text);

            moneyCount = dataPrefab.money;
            karma = dataPrefab.karma;

            foreach (StartItemInfo item in dataPrefab.items)
            {
                if (ItemsManager.instance.ExistsItem(item.idItem))
                {
                    items.Add(item.idItem, item.amount);
                }
                else
                {
                    Debug.LogWarning("Item with id: " + item.idItem + " in startInventory.txt, doesn't exist.");
                }
            }
        }
    }
}