using UnityEngine;
using TMPro;

public class ManageShopGrid : ManageProductsInGrid
{
    public override GameObject AddItem(string newItem)
    {
        GameObject shopSlot = base.AddItem(newItem);
        int id = int.Parse(newItem);

        // Don't need comprobation 100% the item is inside
        Product product = ProductsManager.instance.GetProductInfo(id).Product;
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
