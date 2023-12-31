using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private ManageShopGrid _manageShopGrid;

    private readonly List<int> _cart = new();

    private float _moneyInCart = 0f;

    private void Start()
    {
        gameObject.SetActive(false);
        UIManager.instance.AddUI(UIType.PANEL_SHOP, gameObject);
    }

    public void OpenTrade()
    {
        UIManager.instance.ActivePanelShop();
        foreach(VendorItem vendorProduct in VendorManager.instance.VendorItems)
        {
            GameObject product = ProductsManager.instance.SearchProductByID(vendorProduct.idProduct);
            ProductInfo productInfo = product.GetComponent<ProductInfo>();
            GameObject shopSlot = _manageShopGrid.AddItem(product);
            
            shopSlot.GetComponentInChildren<SelectedProduct>().ShopUI = this; // this implementation avoids
                                                                              // ManageShopGrid to know anything about
                                                                              // this class
            
            _manageShopGrid.ModifyQuantity(vendorProduct.idProduct, vendorProduct.quantity);
            _manageShopGrid.ModifyPrice(productInfo.Product.id, productInfo.Product.buyPrice);
        }
    }

    public void Exit()
    {
        UIManager.instance.FreeUI(UIType.PANEL_SHOP);
        _cart.Clear();
        _manageShopGrid.CleanGrid();
    }

    public void Confirm()
    {
        // Trade money and add the new items in the inventory
        foreach (int productId in _cart)
        {
            int quantity = VendorManager.instance.SearchVendorItem(productId).quantity;
            InventoryManager.instance.ModifyInventory(productId, quantity); // TODO use a timer to get it at some point
        }
        PlayerStats.instance.Money += _moneyInCart;
        _cart.Clear();
        _manageShopGrid.CleanGrid();
        UIManager.instance.FreeUI(UIType.PANEL_SHOP);
    }

    public void AddToCart(int idProduct)
    {
        if (!_cart.Contains(idProduct))
        {
            Debug.Log("Added " + idProduct);
            _moneyInCart += ProductsManager.instance.GetProductInfo(idProduct).Product.buyPrice;
            Debug.Log("Money " + _moneyInCart);
            _cart.Add(idProduct);
        }
    }

    public void DeleteFromCart(int deleteProduct)
    {
        if (!_cart.Contains(deleteProduct))
        {
            Debug.Log("Remove " + deleteProduct);
            _moneyInCart -= ProductsManager.instance.GetProductInfo(deleteProduct).Product.buyPrice;
            Debug.Log("Money " + _moneyInCart);
            _cart.Remove(deleteProduct);
        }
    }
}
