[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int actualSegment;
    public QuestSegmentState[] segmentsStates;

    public QuestData(QuestState state, int actualSegment, QuestSegmentState[] segmentsStates)
    {
        this.state = state;
        this.actualSegment = actualSegment;
        this.segmentsStates = segmentsStates;
    }
}