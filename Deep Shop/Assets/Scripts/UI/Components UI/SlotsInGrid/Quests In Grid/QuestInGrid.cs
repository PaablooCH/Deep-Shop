using TMPro;
using UnityEngine;

public class QuestInGrid : MonoBehaviour
{
    private string _idQuest;
    private int _segmentPostion;

    private void OnDestroy()
    {
        GameEventsMediator.instance.questEvents.onAdvanceQuest -= AdvanceQuest;

        GameEventsMediator.instance.questEvents.onUpdateQuestTooltip -= UpdateQuestTooltip;
    }

    private void AdvanceQuest(string idQuest)
    {
        if (idQuest == _idQuest)
        {
            Quest quest = QuestManager.instance.GetQuestById(_idQuest);
            _segmentPostion++;
            if (_segmentPostion < quest.QuestInfo.Segments.Length)
            {
                QuestSegment questSegment = quest.GetQuestSegment(_segmentPostion);
                TextMeshProUGUI text = transform.Find("Segment Description").GetComponent<TextMeshProUGUI>();
                text.text = questSegment.Description;
            }
        }
    }

    public void Initialize(string idQuest)
    {
        _idQuest = idQuest;
        _segmentPostion = 0;

        GameEventsMediator.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsMediator.instance.questEvents.onUpdateQuestTooltip += UpdateQuestTooltip;
    }

    public void CanFinishQuestSlot()
    {
        // Slot description
        TextMeshProUGUI text = transform.Find("Segment Description").GetComponent<TextMeshProUGUI>();
        text.text = "Quest can be finished.";

        // Disable tooltip
        TooltipGameObjectTrigger tooltipTrigger = GetComponent<TooltipGameObjectTrigger>();
        tooltipTrigger.enabled = false;
    }

    private void UpdateQuestTooltip(string idQuest, GameObject tooltip)
    {
        if (idQuest == _idQuest)
        {
            TooltipGameObjectTrigger tooltipTrigger = GetComponent<TooltipGameObjectTrigger>();
            tooltipTrigger.GameObjectsTooltip = new GameObject[] { tooltip };
        }
    }
}
