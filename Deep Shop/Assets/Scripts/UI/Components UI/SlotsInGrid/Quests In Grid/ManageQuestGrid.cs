using TMPro;
using UnityEngine;

public class ManageQuestGrid : ManageSlotsInGrid
{
    private void Start()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += StartQuestInProgress;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= StartQuestInProgress;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    public override GameObject AddItem(string newQuestId)
    {
        if (!_productsInGrid.ContainsKey(newQuestId))
        {
            // Instatiate slot
            GameObject gridObject = Instantiate(_slotPrefab, _gridTransform);

            gridObject.GetComponent<QuestInGrid>().Initialize(newQuestId);
            Quest quest = QuestManager.instance.GetQuestById(newQuestId);

            // Set Quest Name
            TextMeshProUGUI text = gridObject.transform.Find("Name Quest").GetComponent<TextMeshProUGUI>();
            text.text = quest.QuestInfo.NameQuest;

            // Set First Segment Quest
            text = gridObject.transform.Find("Segment Description").GetComponent<TextMeshProUGUI>();
            text.text = quest.QuestInfo.Segments[0].GetComponent<QuestSegment>().Description;

            TooltipGameObjectTrigger tooltipTrigger = gridObject.GetComponent<TooltipGameObjectTrigger>();
            tooltipTrigger.Header = quest.QuestInfo.NameQuest;


            _productsInGrid.Add(newQuestId, gridObject);
        }
        return _productsInGrid[newQuestId];
    }

    private void StartQuestInProgress(Quest quest)
    {
        string idQuest = quest.QuestInfo.IdQuest;

        // Add quest with state IN_PROGRESS
        if (quest.State == QuestState.IN_PROGRESS)
        {
            AddItem(idQuest);
        }
        else if (quest.State == QuestState.CAN_FINISH)
        {
            if (!_productsInGrid.ContainsKey(idQuest))
            {
                AddItem(idQuest);
            }
            GameObject slot = _productsInGrid[idQuest];
            slot.GetComponent<QuestInGrid>().CanFinishQuestSlot();
        }
    }

    private void FinishQuest(string idQuest)
    {
        RemoveItem(idQuest);
    }
}
