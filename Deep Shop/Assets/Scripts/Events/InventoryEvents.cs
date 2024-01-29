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

    public event Action<string, int> onModifyQuantity;
    public void ModifyQuantity(string idItem, int quantity)
    {
        if (onModifyQuantity != null)
        {
            onModifyQuantity(idItem, quantity);
        }
    }

    public event Action<string> onRemoveItem;
    public void RemoveItem(string idItem)
    {
        if (onRemoveItem != null)
        {
            onRemoveItem(idItem);
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
