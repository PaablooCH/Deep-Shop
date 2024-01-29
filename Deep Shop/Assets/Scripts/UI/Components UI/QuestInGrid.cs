using UnityEngine;
using TMPro;

public class QuestInGrid : MonoBehaviour
{
    private string _idQuest;
    private int _segmentPostion;

    private void Start()
    {
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
    }

    private void AdvanceQuest(string idQuest)
    {
        if (idQuest == _idQuest)
        {
            Quest quest = QuestManager.instance.GetQuestById(_idQuest);
            _segmentPostion++;
            if (_segmentPostion < quest.QuestInfo.segments.Length)
            {
                TextMeshProUGUI text = transform.Find("Segment Description").GetComponent<TextMeshProUGUI>();
                text.text = quest.QuestInfo.segments[_segmentPostion].GetComponent<QuestSegment>().Description;
            }
        }
    }

    public void Initialize(string idQuest)
    {
        _idQuest = idQuest;
        _segmentPostion = 0;
    }
}
