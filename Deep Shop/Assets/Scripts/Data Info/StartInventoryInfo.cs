[System.Serializable]
public class StartItemInfo
{
    public string idItem;
    public int amount;
}

[System.Serializable]
public class StartInventoryInfo
{
    public float money;
    public float karma;
    public StartItemInfo[] items;
}
