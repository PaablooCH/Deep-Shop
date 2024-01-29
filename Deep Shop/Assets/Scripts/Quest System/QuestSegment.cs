using UnityEngine;

public abstract class QuestSegment : MonoBehaviour
{
    [SerializeField] private string _description;

    private bool isFinished = false;
    private string _questId;

    public string Description { get => _description; set => _description = value; }

    public void InitializeSegment(string questId)
    {
        _questId = questId;
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
}
