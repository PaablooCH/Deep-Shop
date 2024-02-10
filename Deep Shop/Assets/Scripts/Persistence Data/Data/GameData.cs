using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    // Adapt inventory to manage more than 1 player
    public PlayerData playerData;
    public InventoryData inventoryData;
    public Dictionary<string, QuestData> questsData = new();

    public GameData()
    {
        playerData = new PlayerData();
        inventoryData = new InventoryData();
    }
}
