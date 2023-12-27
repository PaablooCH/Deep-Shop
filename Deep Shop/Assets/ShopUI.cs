using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private GameObject _panelShop;
    [SerializeField]
    private GameObject _buttonShop;

    private void Start()
    {
        if (_panelShop != null)
        {
            _panelShop.SetActive(false);
            UIManager.instance.AddUI(UIType.PANEL_SHOP, _panelShop);
        }
        if (_buttonShop != null)
        {
            _panelShop.SetActive(false);
            UIManager.instance.AddUI(UIType.BUTTON_SHOP, _buttonShop);
        }
    }

    public void ClickShopButton()
    {
        if (_panelShop != null)
        {
            UIManager.instance.ActivePanelShop();
        }
    }

    public void Exit()
    {
        if (_buttonShop != null)
        {
            UIManager.instance.FreeUI(UIType.PANEL_SHOP);
        }
    }

    public void Confirm()
    {
        UIManager.instance.FreeUI(UIType.PANEL_SHOP);
        UIManager.instance.FreeUI(UIType.BUTTON_SHOP);

        // Trade money and add the new items in the inventory
    }
}
