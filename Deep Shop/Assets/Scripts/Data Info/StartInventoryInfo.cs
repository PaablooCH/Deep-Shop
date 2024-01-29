[System.Serializable]
public class StartInventoryInfo
{
    public string idItem;
    public int amount;
}

[System.Serializable]
public class StartInventoryArray
{
    public StartInventoryInfo[] startInventory;
}
