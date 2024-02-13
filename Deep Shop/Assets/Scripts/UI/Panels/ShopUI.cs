using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour, IUIGameObject, IUIConfirmation, IUIReject
{
    // UI elements
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private ManageShopGrid _manageShopGrid;
    [SerializeField] private PackageWithItems _deliveryPlace;

    [SerializeField] private BuyInteraction _buyInteraction;

    VendorItemToSell _actualVendorProducts;
    
    private List<string> _cart = new(); // stores the idItems that we want to buy

    private float _moneyInCart = 0f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(GameObject vendor)
    {
        if (vendor.TryGetComponent(out VendorItemToSell component))
        {
            _actualVendorProducts = component;
            UIManager.instance.ActiveUI(UIs.SHOP);

            foreach (ItemQuantity vendorProduct in _actualVendorProducts.VendorProducts)
            {
                string idItem = vendorProduct.Item.GetItemId();
                
                GameObject shopSlot = _manageShopGrid.AddItem(idItem);

                shopSlot.GetComponentInChildren<SelectedShopItem>().ShopUI = this; // this implementation avoids
                                                                                      // ManageShopGrid to know anything about
                                                                                      // this class

                _manageShopGrid.ModifyQuantity(idItem, vendorProduct.Quantity);
                _manageShopGrid.ModifyPrice(idItem, vendorProduct.Quantity * vendorProduct.Item.ItemInfo.BuyPrice);
            }
        }
    }

    public void Exit()
    {
        UIManager.instance.FreeUI();
        _cart.Clear();
        _manageShopGrid.CleanGrid();
    }

    public void Confirm()
    {
        // Trade money and add the new items to the deliverManager
        foreach (string itemId in _cart)
        {
            int quantity = _actualVendorProducts.SearchVendorProduct(itemId).Quantity;
            Item itemToDeliver = ItemsManager.instance.GetItemByID(itemId);
            _deliveryPlace.PackagesWaiting.Add(new DeliverObject(10, new ItemQuantity(itemToDeliver, quantity)));
        }
        PlayerManager.instance.GetPlayerInventory().Money -= _moneyInCart;
        _cart.Clear();
        _manageShopGrid.CleanGrid();
        _buyInteraction.EndInteraction();
        UIManager.instance.FreeUI();
    }

    public void Reject()
    {
        _buyInteraction.EndInteraction();
        UIManager.instance.FreeUI();
    }

    public void AddToCart(string idItem)
    {
        if (!_cart.Contains(idItem))
        {
            _moneyInCart += ItemsManager.instance.GetItemByID(idItem).ItemInfo.BuyPrice * 
                _actualVendorProducts.SearchVendorProduct(idItem).Quantity;
            UpdateCostDependencies();
            _cart.Add(idItem);
            if (_button.interactable == false)
            {
                _button.interactable = true;
            }
        }
    }

    public void DeleteFromCart(string deleteProduct)
    {
        if (_cart.Contains(deleteProduct))
        {
            _moneyInCart -= ItemsManager.instance.GetItemByID(deleteProduct).ItemInfo.BuyPrice;
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
        if (_moneyInCart > PlayerManager.instance.GetPlayerInventory().Money)
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
