using System;

public class CraftEvents
{
    public event Action<int, int> onItemCrafted;
    public void ItemCrafted(int id, int quantity)
    {
        if (onItemCrafted != null)
        {
            onItemCrafted(id, quantity);
        }
    }
}
