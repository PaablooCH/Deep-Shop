using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageItemsInGrid : ManageSlotsInGrid
{
    public override GameObject AddItem(string newItemId)
    {
        if (!_productsInGrid.ContainsKey(newItemId))
        {
            Item newItem = ItemsManager.instance.GetItemByID(newItemId);
            GameObject gridObject = Instantiate(_slotPrefab, _gridTransform);
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = newItem.ItemInfo.Sprite;
            image.color = newItem.ItemInfo.Color;
            image.enabled = true;
            _productsInGrid.Add(newItemId, gridObject);
        }
        return _productsInGrid[newItemId];
    }

    public void ModifyQuantity(string modifiedItem, int amount)
    {
        if (_productsInGrid.TryGetValue(modifiedItem, out GameObject slot))
        {
            TextMeshProUGUI text = slot.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            text.text = amount.ToString();
        }
    }
}
