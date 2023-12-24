using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // UI elements
    [SerializeField]
    private GameObject panelShop;
    [SerializeField]
    private GameObject buttonShop;

    private void Start()
    {
        if (panelShop != null)
        {
            panelShop.SetActive(false);
            UIManager.instance.AddUI(UIType.PANEL_SHOP, panelShop);
        }
        if (buttonShop != null)
        {
            panelShop.SetActive(false);
            UIManager.instance.AddUI(UIType.BUTTON_SHOP, buttonShop);
        }
    }

    public void ClickShopButton()
    {
        if (panelShop != null)
        {
            UIManager.instance.ActivePanelShop();
        }
    }

    public void Exit()
    {
        if (buttonShop != null)
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
