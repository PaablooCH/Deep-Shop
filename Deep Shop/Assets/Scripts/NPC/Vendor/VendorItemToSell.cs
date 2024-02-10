using System.Collections.Generic;
using UnityEngine;

public class VendorItemToSell : MonoBehaviour
{
    [SerializeField] private int _numberToSell = 3;

    private ItemQuantity[] _vendorProducts;

    public ItemQuantity[] VendorProducts { get => _vendorProducts; set => _vendorProducts = value; }

    private void Start()
    {
        // Set the max numbers of items to sell
        int numberToSell = _numberToSell < ItemsManager.instance.HowManyItemsExist() ?
        _numberToSell : ItemsManager.instance.HowManyItemsExist();
        // Generate how many items will sell with a random generator
        int howMany = UtilsNumberGenerator.GenerateNumberWithWeight(1, numberToSell, 2, 1);
        List<Item> products = new();
        int i = 0;
        // Fill the list with random values
        while (i < howMany)
        {
            Item item = ItemsManager.instance.RandomItem();
            if (!products.Contains(item))
            {
                products.Add(item);
                i++;
            }
        }

        _vendorProducts = new ItemQuantity[howMany];
        for (i = 0; i < howMany; i++)
        {
            // Specify the quantity of each item
            int quantity = UtilsNumberGenerator.GenerateNumberWithWeight(1, 5, 3, 1);
            _vendorProducts[i] = new ItemQuantity(products[i], quantity);
        }
    }

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
