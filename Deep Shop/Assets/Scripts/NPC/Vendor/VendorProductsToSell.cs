using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorProduct
{
    public int idProduct;
    public int quantity;

    public VendorProduct(int idProduct, int quantity)
    {
        this.idProduct = idProduct;
        this.quantity = quantity;
    }
}

public class VendorProductsToSell : MonoBehaviour
{
    private VendorProduct[] _vendorProducts;

    public VendorProduct[] VendorProducts { get => _vendorProducts; set => _vendorProducts = value; }

    public VendorProduct SearchVendorProduct(int id)
    {
        foreach (VendorProduct item in _vendorProducts)
        {
            if (item.idProduct == id)
            {
                return item;
            }
        }
        return null;
    }
}
