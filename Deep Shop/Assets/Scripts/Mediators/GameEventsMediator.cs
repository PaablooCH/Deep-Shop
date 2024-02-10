using UnityEngine;

public class GameEventsMediator : MonoBehaviour
{
    #region Singleton
    public static GameEventsMediator instance;

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
    public NPCEvents npcEvents;
    public DialogueEvents dialogueEvents;

    private void InitializeEvents()
    {
        inventoryEvent = new InventoryEvents();
        craftEvents = new CraftEvents();
        questEvents = new QuestEvents();
        npcEvents = new NPCEvents();
        dialogueEvents = new DialogueEvents();
    }
}
