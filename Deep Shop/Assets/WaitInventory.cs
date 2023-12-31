using System.Collections;
using System.Collections.Generic;
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

    public bool InventorySlotReady { get => _inventorySlotReady; set => _inventorySlotReady = value; }

    // Update is called once per frame
    void Update()
    {
        if (_inventorySlotReady)
        {
            InventoryManager.instance.StartItems();
            enabled = false;
        }
    }
}
