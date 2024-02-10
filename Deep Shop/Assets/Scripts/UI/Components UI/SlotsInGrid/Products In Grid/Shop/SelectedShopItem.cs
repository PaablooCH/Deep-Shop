using UnityEngine;

public class SelectedShopItem : MonoBehaviour
{  
    private ShopUI _shopUI;

    private bool _addedToCart = false;
    private string _itemId;

    public ShopUI ShopUI { get => _shopUI; set => _shopUI = value; }
    public string ItemId { get => _itemId; set => _itemId = value; }

    public void ModifyCart()
    {
        _addedToCart = !_addedToCart;
        if (_addedToCart)
        {
            _shopUI.AddToCart(_itemId);
        }
        else
        {
            _shopUI.DeleteFromCart(_itemId);
        }
    }
}
