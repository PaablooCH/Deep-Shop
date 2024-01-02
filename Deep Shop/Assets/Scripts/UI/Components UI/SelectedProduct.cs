using UnityEngine;

public class SelectedProduct : MonoBehaviour
{  
    private ShopUI _shopUI;
    private ManageShopGrid _manageShopGrid;

    private bool _addedToCart = false;

    public ShopUI ShopUI { get => _shopUI; set => _shopUI = value; }
    public ManageShopGrid ManageShopGrid { get => _manageShopGrid; set => _manageShopGrid = value; }

    public void ModifyCart()
    {
        _addedToCart = !_addedToCart;
        if (_addedToCart)
        {
            int index = transform.GetSiblingIndex();
            _shopUI.AddToCart(_manageShopGrid.GetIdProductFromChild(index));
        }
        else
        {
            _shopUI.DeleteFromCart(_manageShopGrid.GetIdProductFromChild(transform.GetSiblingIndex()));
        }
    }
}
