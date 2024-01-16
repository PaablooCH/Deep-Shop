[System.Serializable]
public class StartInventoryInfo
{
    public int productID;
    public int amount;
}

[System.Serializable]
public class StartInventoryArray
{
    public StartInventoryInfo[] startInventory;
}
