using UnityEngine;

public class VendorProductsToSell : MonoBehaviour
{
    private ProductQuantity[] _vendorProducts;

    public ProductQuantity[] VendorProducts { get => _vendorProducts; set => _vendorProducts = value; }

    public ProductQuantity SearchVendorProduct(int id)
    {
        foreach (ProductQuantity item in _vendorProducts)
        {
            if (item.idProduct == id)
            {
                return item;
            }
        }
        return null;
    }
}
