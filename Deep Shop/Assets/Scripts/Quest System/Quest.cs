using UnityEngine;

public class Quest
{
    private QuestInfoSO _questInfo;

    private QuestState _state;
    private int _currentQuestSegmentIndex;

    public QuestInfoSO QuestInfo { get => _questInfo; set => _questInfo = value; }
    public QuestState State { get => _state; set => _state = value; }

    public Quest(QuestInfoSO questInfo)
    {
        _questInfo = questInfo;
        _state = QuestState.REQUIREMENTS_NOT_MET;
        _currentQuestSegmentIndex = 0;
    }

    public void MoveToNextSegment()
    {
        _currentQuestSegmentIndex++;
    }

    public bool CurrentSegmentExists()
    {
        return _currentQuestSegmentIndex < _questInfo.Segments.Length;
    }

    public void InstantiateSegment(Transform parent)
    {
        GameObject segment = GetCurrentSegment();
        if (segment != null)
        {
            GameObject newSegment = Object.Instantiate(segment, parent);
            QuestSegment questSegment = newSegment.GetComponent<QuestSegment>();
            questSegment.InitializeSegment(_questInfo.IdQuest);
        }
    }

    private GameObject GetCurrentSegment()
    {
        GameObject currentSegement = null;
        if (CurrentSegmentExists())
        {
            currentSegement = _questInfo.Segments[_currentQuestSegmentIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get the current segment but it seems that all the segments are achieved. " +
                "ID: " + _questInfo.IdQuest + ", QuestName: " + _questInfo.NameQuest + " and actualSegment = " + _currentQuestSegmentIndex);
        }
        return currentSegement;
    }
}
