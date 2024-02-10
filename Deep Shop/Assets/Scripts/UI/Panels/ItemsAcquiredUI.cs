using UnityEngine;

public class ItemsAcquiredUI : MonoBehaviour, IUIGameObject, IUIConfirmation
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
            UIManager.instance.ActiveUI(UIs.ITEM_ACQ);
            foreach (ItemQuantity productQuantity in _packagesWithItems.Package)
            {
                _manageItemsAcquired.AddItem(productQuantity.Item.GetItemId());
                _manageItemsAcquired.ModifyQuantity(productQuantity.Item.GetItemId(), productQuantity.Quantity);
            }
        }
    }

    public void Confirm()
    {
        if (_packagesWithItems)
        {
            foreach (ItemQuantity productQuantity in _packagesWithItems.PickPackages())
            {
                PlayerManager.instance.GetPlayerInventory().ModifyInventory(productQuantity.Item.GetItemId(), productQuantity.Quantity);
            }
            UIManager.instance.FreeUI();
        }
    }
}
