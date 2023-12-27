using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopGameObject;
    
    private ShopSlot _shop;

    private void Start()
    {
        _shop = _shopGameObject.GetComponentInChildren<ShopSlot>();
    }

    public void ModifyCart(bool state)
    {
        if (state)
        {
            _shop.AddToCart(_shop.GetIdProductFromChild(transform.GetSiblingIndex()));
        }
        else
        {
            _shop.DeleteFromCart(_shop.GetIdProductFromChild(transform.GetSiblingIndex()));
        }
    }
}