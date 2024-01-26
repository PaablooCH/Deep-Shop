using System;
using UnityEngine;

public class InventoryEvents
{
    public delegate GameObject OnAddItem(GameObject product);
    public event OnAddItem onAddItem;
    public void AddItem(GameObject product)
    {
        if (onAddItem != null)
        {
            onAddItem(product);
        }
    }

    public event Action<int, int> onModifyQuantity;
    public void ModifyQuantity(int id, int quantity)
    {
        if (onModifyQuantity != null)
        {
            onModifyQuantity(id, quantity);
        }
    }

    public event Action<int> onRemoveItem;
    public void RemoveItem(int id)
    {
        if (onRemoveItem != null)
        {
            onRemoveItem(id);
        }
    }

    public Action<float, float> onKarmaChanged;
    public void KarmaChanged(float newKarma, float oldKarma)
    {
        if (onKarmaChanged != null)
        {
            onKarmaChanged(newKarma, oldKarma);
        }
    }

    public Action<float> onMoneyChanged;
    public void MoneyChange(float money)
    {
        if (onMoneyChanged != null)
        {
            onMoneyChanged(money);
        }
    }
}
