using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour, IUIProduct, IUIConfirmation, IUIReject
{
    // UI elements
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private ManageShopGrid _manageShopGrid;

    [SerializeField] private BuyInteraction _buyInteraction;

    VendorProductsToSell _actualVendorProducts;
    private readonly List<int> _cart = new();

    private float _moneyInCart = 0f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(GameObject vendor)
    {
        if (vendor.TryGetComponent(out VendorProductsToSell component))
        {
            _actualVendorProducts = component;
            CanvasManager.instance.ActiveUI(UIs.SHOP);

            foreach (ProductQuantity vendorProduct in _actualVendorProducts.VendorProducts)
            {
                ProductInfo productInfo = ProductsManager.instance.GetProductInfo(vendorProduct.idProduct);
                
                GameObject shopSlot = _manageShopGrid.AddItem(vendorProduct.idProduct.ToString());

                shopSlot.GetComponentInChildren<SelectedShopProduct>().ShopUI = this; // this implementation avoids
                                                                                  // ManageShopGrid to know anything about
                                                                                  // this class

                _manageShopGrid.ModifyQuantity(vendorProduct.idProduct, vendorProduct.quantity);
                _manageShopGrid.ModifyPrice(productInfo.Product.id, productInfo.Product.buyPrice);
            }
        }
    }

    public void Exit()
    {
        CanvasManager.instance.FreeUI();
        _cart.Clear();
        _manageShopGrid.CleanGrid();
    }

    public void Confirm()
    {
        // Trade money and add the new items to the deliverManager
        foreach (int productId in _cart)
        {
            int quantity = _actualVendorProducts.SearchVendorProduct(productId).quantity;
            DeliverManager.instance.Packages.Add(new DeliverObject(10, new ProductQuantity(productId, quantity)));
        }
        InventoryManager.instance.Money -= _moneyInCart;
        _cart.Clear();
        _manageShopGrid.CleanGrid();
        _buyInteraction.EndInteraction();
        CanvasManager.instance.FreeUI();
    }

    public void Reject()
    {
        _buyInteraction.EndInteraction();
        CanvasManager.instance.FreeUI();
    }

    public void AddToCart(int idProduct)
    {
        if (!_cart.Contains(idProduct))
        {
            _moneyInCart += ProductsManager.instance.GetProductInfo(idProduct).Product.buyPrice;
            UpdateCostDependencies();
            _cart.Add(idProduct);
            if (_button.interactable == false)
            {
                _button.interactable = true;
            }
        }
    }

    public void DeleteFromCart(int deleteProduct)
    {
        if (_cart.Contains(deleteProduct))
        {
            _moneyInCart -= ProductsManager.instance.GetProductInfo(deleteProduct).Product.buyPrice;
            UpdateCostDependencies();
            _cart.Remove(deleteProduct);
            if (_cart.Count == 0)
            {
                _button.interactable = false;
            }
        }
    }

    private void UpdateCostDependencies()
    {
        _cost.text = _moneyInCart.ToString("0.00") + " G";
        if (_moneyInCart > InventoryManager.instance.Money)
        {
            _button.interactable = false;
            _cost.color = Color.red;
        }
        else
        {
            _cost.color = Color.green;
            _button.interactable = true;
        }
    }
}
