using UnityEngine;

public class VendorProductsToSell : MonoBehaviour
{
    private ItemQuantity[] _vendorProducts;

    public ItemQuantity[] VendorProducts { get => _vendorProducts; set => _vendorProducts = value; }

    public ItemQuantity SearchVendorProduct(string id)
    {
        foreach (ItemQuantity itemQuantity in _vendorProducts)
        {
            if (itemQuantity.Item.GetItemId() == id)
            {
                return itemQuantity;
            }
        }
        return null;
    }
}
