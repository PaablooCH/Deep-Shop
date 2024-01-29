using UnityEngine;
using TMPro;

public class ManageShopGrid : ManageItemsInGrid
{
    public override GameObject AddItem(string newItem)
    {
        GameObject shopSlot = base.AddItem(newItem);

        // Don't need comprobation 100% the item is inside
        Item item = ItemsManager.instance.GetItemByID(newItem);
        SelectedShopProduct selectedProduct = shopSlot.GetComponentInChildren<SelectedShopProduct>();
        selectedProduct.ItemId = newItem;

        TooltipTrigger tooltipTrigger = shopSlot.GetComponent<TooltipTrigger>();
        tooltipTrigger.Header = item.ItemInfo.NameItem;
        tooltipTrigger.Body = item.ItemInfo.Description;

        return shopSlot;
    }

    public void ModifyPrice(string modifiedItem, float price)
    {
        if (_productsInGrid.TryGetValue(modifiedItem, out GameObject slot))
        {
            TextMeshProUGUI text = slot.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            text.text = price.ToString("0.0") + " G";
        }
    }
}
