using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region Singleton
    public static GameEventManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;

        InitializeEvents();
    }
    #endregion

    public InventoryEvents inventoryEvent;
    public CraftEvents craftEvents;

    private void InitializeEvents()
    {
        inventoryEvent = new InventoryEvents();
        craftEvents = new CraftEvents();
    }
}
