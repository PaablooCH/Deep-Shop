using UnityEngine;

public class ItemsAcquiredUI : MonoBehaviour, IUIProduct, IUIConfirmation
{
    // UI elements
    [SerializeField] private ManageItemsAcquiredInGrid _manageItemsAcquired;

    private PackageWithItems _packagesWithItems;

    private void Start()
    {
        gameObject.SetActive(false);
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
            CanvasManager.instance.ActiveUI(gameObject);
            foreach (ProductQuantity productQuantity in _packagesWithItems.Package)
            {
                GameObject product = ProductsManager.instance.SearchProductByID(productQuantity.idProduct);
                _manageItemsAcquired.AddItem(product);
                _manageItemsAcquired.ModifyQuantity(productQuantity.idProduct, productQuantity.quantity);
            }
        }
    }

    public void Confirm()
    {
        if (_packagesWithItems)
        {
            foreach (ProductQuantity productQuantity in _packagesWithItems.PickPackages())
            {
                InventoryManager.instance.ModifyInventory(productQuantity.idProduct, productQuantity.quantity);
            }
            CanvasManager.instance.FreeUI();
        }
    }
}
