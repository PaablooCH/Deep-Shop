using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private GameObject _panelShop;

    private ShopSlot _shopSlot;

    private void Start()
    {
        if (_panelShop != null)
        {
            _panelShop.SetActive(false);
            UIManager.instance.AddUI(UIType.PANEL_SHOP, _panelShop);
            _shopSlot = _panelShop.transform.Find("Shop Panel").gameObject.GetComponent<ShopSlot>();
        }
    }

    public void OpenTrade()
    {
        if (_panelShop != null) // this can be generated once and opentrade only open the Panel
        {
            UIManager.instance.ActivePanelShop();
            foreach(VendorItem vendorProduct in VendorManager.instance.VendorItems)
            {
                GameObject product = ProductsManager.instance.SearchProductByID(vendorProduct.idProduct);
                ProductInfo productInfo = product.GetComponent<ProductInfo>();
                _shopSlot.AddItem(product);
                _shopSlot.ModifyQuantity(vendorProduct.idProduct, vendorProduct.quantity);
                _shopSlot.ModifyPrice(productInfo.Product.id, productInfo.Product.buyPrice);
            }
        }
    }

    public void Exit()
    {
        UIManager.instance.FreeUI(UIType.PANEL_SHOP);
    }

    public void Confirm()
    {
        UIManager.instance.FreeUI(UIType.PANEL_SHOP);

        // Trade money and add the new items in the inventory
    }
}
