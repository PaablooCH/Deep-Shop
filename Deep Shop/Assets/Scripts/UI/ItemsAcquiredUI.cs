using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAcquiredUI : MonoBehaviour, IUIConfirmation
{
    // UI elements
    [SerializeField]
    private ManageItemsAcquiredInGrid _manageItemsAcquired;

    private PackageWithItems _packagesWithItems;

    private void Start()
    {
        gameObject.SetActive(false);
        CanvasManager.instance.AddUI(UIType.ITEMS_ACQUIRED, gameObject);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void OpenUI(GameObject go)
    {
        if (go.TryGetComponent(out PackageWithItems component))
        {
            _packagesWithItems = component;
            CanvasManager.instance.ActivePanelItemsAquired();
            foreach (ProductQuantity productQuantity in _packagesWithItems.Package)
            {
                GameObject product = ProductsManager.instance.SearchProductByID(productQuantity.IdProduct);
                _manageItemsAcquired.AddItem(product);
                _manageItemsAcquired.ModifyQuantity(productQuantity.IdProduct, productQuantity.Quantity);
            }
        }
    }

    public void Confirm()
    {
        if (_packagesWithItems)
        {
            foreach (ProductQuantity productQuantity in _packagesWithItems.PickPackages())
            {
                InventoryManager.instance.ModifyInventory(productQuantity.IdProduct, productQuantity.Quantity);
            }
            CanvasManager.instance.FreeUI(UIType.ITEMS_ACQUIRED);
        }
    }
}
