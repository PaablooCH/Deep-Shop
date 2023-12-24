using System.Collections;
using System.Collections.Generic;

public class ShopSlot : ManageProductsInGrid
{
    private List<ProductType> cart = new();

    public void AddToCart(ProductType newProduct)
    {
        if (!cart.Contains(newProduct) && newProduct != ProductType.NONE)
        {
            cart.Add(newProduct);
        }
    }

    public void DeleteFromCart(ProductType deleteProduct)
    {
        cart.Remove(deleteProduct);
    }
}
