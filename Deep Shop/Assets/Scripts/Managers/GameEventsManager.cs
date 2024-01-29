using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    #region Singleton
    public static GameEventsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Game Events Manager singleton already exists.");
            return;
        }
        instance = this;

        InitializeEvents();
    }
    #endregion

    public InventoryEvents inventoryEvent;
    public CraftEvents craftEvents;
    public QuestEvents questEvents;
    public DialogueEvents dialogueEvents;

    private void InitializeEvents()
    {
        inventoryEvent = new InventoryEvents();
        craftEvents = new CraftEvents();
        questEvents = new QuestEvents();
        dialogueEvents = new DialogueEvents();
    }
}
