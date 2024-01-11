using UnityEngine;
using TMPro;

public class ManageShopGrid : ManageSlotsInGrid
{
    public override GameObject AddItem(GameObject newItem)
    {
        GameObject shopSlot = base.AddItem(newItem);

        // Don't need comprobation 100% the item is inside
        SelectedShopProduct selectedProduct =  shopSlot.GetComponentInChildren<SelectedShopProduct>();
        selectedProduct.ManageShopGrid = this;
        return shopSlot;
    }

    public void ModifyPrice(int modifiedItem, float price)
    {
        int index = _productsInGrid.FindIndex((idProduct) => idProduct == modifiedItem);
        TextMeshProUGUI text = _gridTransform.GetChild(index).transform.Find("Price").GetComponent<TextMeshProUGUI>();
        text.text = price.ToString("0.0") + " G";
    }
}
