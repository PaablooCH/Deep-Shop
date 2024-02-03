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

[System.Serializable]
public class PlayerData
{
    public Vector3 position;

    public PlayerData()
    {
        position = Vector3.zero;
    }
}

[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int actualSegment;
    public QuestSegmentState[] segmentsStates;

    public QuestData(QuestState state, int actualSegment, QuestSegmentState[] segmentsStates)
    {
        this.state = state;
        this.actualSegment = actualSegment;
        this.segmentsStates = segmentsStates;
    }
}

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public InventoryData inventoryData;
    public Dictionary<string, QuestData> questsData = new();

    public GameData()
    {
        playerData = new PlayerData();
        inventoryData = new InventoryData();
    }
}
