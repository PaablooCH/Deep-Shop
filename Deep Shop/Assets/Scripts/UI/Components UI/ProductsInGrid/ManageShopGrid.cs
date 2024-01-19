using UnityEngine;
using TMPro;

public class ManageShopGrid : ManageSlotsInGrid
{
    public override GameObject AddItem(GameObject newItem)
    {
        GameObject shopSlot = base.AddItem(newItem);

        // Don't need comprobation 100% the item is inside
        Product product = newItem.GetComponent<ProductInfo>().Product;
        int newId = product.id;
        SelectedShopProduct selectedProduct = shopSlot.GetComponentInChildren<SelectedShopProduct>();
        selectedProduct.ProductId = newId;

        TooltipTrigger tooltipTrigger = shopSlot.GetComponent<TooltipTrigger>();
        tooltipTrigger.Header = product.productName;
        tooltipTrigger.Body = product.description;

        return shopSlot;
    }

    public void ModifyPrice(int modifiedItem, float price)
    {
        if (_productsInGrid.TryGetValue(modifiedItem, out GameObject slot))
        {
            TextMeshProUGUI text = slot.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            text.text = price.ToString("0.0") + " G";
        }
    }
}
