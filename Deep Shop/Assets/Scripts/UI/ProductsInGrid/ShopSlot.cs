using System.Collections;
using System.Collections.Generic;

public class ShopSlot : ManageProductsInGrid
{
    private readonly List<int> _cart = new();

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
