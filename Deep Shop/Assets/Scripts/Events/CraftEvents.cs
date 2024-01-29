using System;

public class CraftEvents
{
    public event Action<string, int> onItemCrafted;
    public void ItemCrafted(string id, int quantity)
    {
        if (onItemCrafted != null)
        {
            onItemCrafted(id, quantity);
        }
    }
}
