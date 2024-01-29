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
    private bool _itemsReady = false;

    public bool InventorySlotReady { get => _inventorySlotReady; set => _inventorySlotReady = value; }
    public bool ItemsReady { get => _itemsReady; set => _itemsReady = value; }

    // Update is called once per frame
    void Update()
    {
        if (_inventorySlotReady)
        {
            StartCoroutine(InventoryManager.instance.StartItemsAsync());
            enabled = false;
        }
    }
}
