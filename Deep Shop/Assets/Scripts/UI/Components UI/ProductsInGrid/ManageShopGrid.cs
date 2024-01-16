using UnityEngine;
using TMPro;

public class ManageShopGrid : ManageSlotsInGrid
{
    public override GameObject AddItem(GameObject newItem)
    {
        GameObject shopSlot = base.AddItem(newItem);

        // Don't need comprobation 100% the item is inside
        int newId = newItem.GetComponent<ProductInfo>().Product.id;
        SelectedShopProduct selectedProduct =  shopSlot.GetComponentInChildren<SelectedShopProduct>();
        selectedProduct.ManageShopGrid = this;
        selectedProduct.ProductId = newId;
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
