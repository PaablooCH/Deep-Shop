using UnityEngine;
using TMPro;

public class ManageQuestGrid : ManageSlotsInGrid
{
    private void Start()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
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

            // TODO Tooltip

            _productsInGrid.Add(newQuestId, gridObject);
        }
        return _productsInGrid[newQuestId];
    }

    private void StartQuest(string idQuest)
    {
        // Add item
        AddItem(idQuest);
    }

    private void FinishQuest(string idQuest)
    {
        RemoveItem(idQuest);
    }
}
