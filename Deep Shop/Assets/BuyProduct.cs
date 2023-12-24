using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    [SerializeField]
    private GameObject shopGameObject;
    
    private ShopSlot shop;

    private void Start()
    {
        shop = shopGameObject.GetComponentInChildren<ShopSlot>();
    }

    public void ModifyCart(bool state)
    {
        if (state)
        {
            shop.AddToCart(shop.GetProductTypeFromChild(transform.GetSiblingIndex()));
        }
        else
        {
            shop.DeleteFromCart(shop.GetProductTypeFromChild(transform.GetSiblingIndex()));
        }
    }
}
