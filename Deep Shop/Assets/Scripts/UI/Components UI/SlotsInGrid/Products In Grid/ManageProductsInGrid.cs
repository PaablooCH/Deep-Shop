using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageProductsInGrid : ManageSlotsInGrid
{
    public override GameObject AddItem(string newItemId)
    {
        // TODO redo when item Scriptable Object created
        int id = int.Parse(newItemId);
        if (!_productsInGrid.ContainsKey(int.Parse(newItemId)))
        {
            GameObject newItem = ProductsManager.instance.SearchProductByID(id);
            GameObject gridObject = Instantiate(_slotPrefab, _gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
            _productsInGrid.Add(id, gridObject);
        }
        return _productsInGrid[id];
    }

    public void ModifyQuantity(int modifiedItem, int amount)
    {
        if (_productsInGrid.TryGetValue(modifiedItem, out GameObject slot))
        {
            TextMeshProUGUI text = slot.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            text.text = amount.ToString();
        }
    }
}
