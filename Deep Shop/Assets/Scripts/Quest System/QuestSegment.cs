using UnityEngine;

public abstract class QuestSegment : MonoBehaviour
{
    [SerializeField] private string _description;
    [SerializeField] protected GameObject _questTooltipPrefab;

    protected string _questId;
    protected int _segmentIndex;

    private bool isFinished = false;

    public string Description { get => _description; set => _description = value; }

    public void InitializeSegment(string questId, int segment, QuestSegmentState segmentState)
    {
        _questId = questId;
        _segmentIndex = segment;
        if (segmentState.state != null && segmentState.state != "")
        {
            LoadSegmentState(segmentState.state);
        }
    }

    protected void FinishQuestSegment()
    {
        if (!isFinished) // TODO can simplify
        {
            isFinished = true;

            // Go to next segment
            GameEventsManager.instance.questEvents.AdvanceQuest(_questId);

            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(QuestSegmentState segmentState)
    {
        GameEventsManager.instance.questEvents.QuestSegmentStateChange(_questId, _segmentIndex, segmentState);
    }

    protected abstract void UpdateTooltip();

    protected abstract void LoadSegmentState(string segmentState);
    protected abstract void SaveSegmentState();
}
