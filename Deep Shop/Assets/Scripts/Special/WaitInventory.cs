using UnityEngine;

public class WaitInventory : MonoBehaviour
{
    #region Singleton
    public static WaitInventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    private bool _inventorySlotReady = false;
    private bool _productsReady = false;

    public bool InventorySlotReady { get => _inventorySlotReady; set => _inventorySlotReady = value; }
    public bool ProductsReady { get => _productsReady; set => _productsReady = value; }

    // Update is called once per frame
    void Update()
    {
        if (_inventorySlotReady && _productsReady)
        {
            StartCoroutine(InventoryManager.instance.StartItemsAsync());
            enabled = false;
        }
    }
}
