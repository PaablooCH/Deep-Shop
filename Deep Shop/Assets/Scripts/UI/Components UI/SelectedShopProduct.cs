using UnityEngine;

public class SelectedShopProduct : MonoBehaviour
{  
    private ShopUI _shopUI;

    private bool _addedToCart = false;
    private int _productId;

    public ShopUI ShopUI { get => _shopUI; set => _shopUI = value; }
    public int ProductId { get => _productId; set => _productId = value; }

    public void ModifyCart()
    {
        _addedToCart = !_addedToCart;
        if (_addedToCart)
        {
            _shopUI.AddToCart(_productId);
        }
        else
        {
            _shopUI.DeleteFromCart(_productId);
        }
    }
}
