using UnityEngine;

public class Quest
{
    private QuestInfoSO _questInfo;

    private QuestState _state;
    private int _currentQuestSegmentIndex;
    private QuestSegmentState[] _segmentsStates; // see Code Explanation #1

    public QuestInfoSO QuestInfo { get => _questInfo; set => _questInfo = value; }
    public QuestState State { get => _state; set => _state = value; }

    public Quest(QuestInfoSO questInfo)
    {
        _questInfo = questInfo;
        _state = QuestState.REQUIREMENTS_NOT_MET;
        _currentQuestSegmentIndex = 0;

        _segmentsStates = new QuestSegmentState[_questInfo.Segments.Length];

        for (int i = 0; i < _segmentsStates.Length; i++)
        {
            _segmentsStates[i] = new QuestSegmentState();
        }
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
            questSegment.InitializeSegment(_questInfo.IdQuest, _currentQuestSegmentIndex, _segmentsStates[_currentQuestSegmentIndex]);
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

    public void UpdateSegmentState(int segment, QuestSegmentState state)
    {
        if (segment < _segmentsStates.Length)
        {
            _segmentsStates[segment] = state;
        }
        else
        {
            Debug.LogWarning("Try to save a Segment State out of bound. Segment: " + segment + ", total states: " + 
                _segmentsStates.Length + ".");
        }
    }

    public void Load(QuestData dataLoaded)
    {
        _state = dataLoaded.state;
        _currentQuestSegmentIndex = dataLoaded.actualSegment;
        _segmentsStates = dataLoaded.segmentsStates;

        if (_segmentsStates.Length != _questInfo.Segments.Length)
        {
            Debug.LogError("The data saved not correspond to the number of segments in this quest with id: " + 
                _questInfo.IdQuest + ". We must redo the save file");
        }
    }

    public QuestData Save()
    {
        return new QuestData(_state, _currentQuestSegmentIndex, _segmentsStates);
    }
}
