using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ShopSlot : ManageProductsInGrid
{
    private readonly List<int> _cart = new();

    public void ModifyPrice(int modifiedItem, float price)
    {
        int index = productsInGrid.FindIndex((idProduct) => idProduct == modifiedItem);
        TextMeshProUGUI text = gridTransform.GetChild(index).transform.Find("Price").GetComponent<TextMeshProUGUI>();
        text.text = price.ToString("0.0") + " G";
    }

    public void AddToCart(int idProduct)
    {
        if (!_cart.Contains(idProduct))
        {
            _cart.Add(idProduct);
        }
    }

    public void DeleteFromCart(int deleteProduct)
    {
        _cart.Remove(deleteProduct);
    }
}
