using System;
using UnityEngine;

public class InventoryEvents
{
    public delegate GameObject OnAddItem(string idItem);
    public event OnAddItem onAddItem;
    public void AddItem(string idItem)
    {
        if (onAddItem != null)
        {
            onAddItem(idItem);
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

    public Action<float> onKarmaChanged;
    public void KarmaChanged(float newKarma)
    {
        if (onKarmaChanged != null)
        {
            onKarmaChanged(newKarma);
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
