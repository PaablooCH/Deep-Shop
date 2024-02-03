using UnityEngine;

public class CraftItemQuest : QuestSegment
{
    [SerializeField] private ItemInfoSO _craftItem;
    [SerializeField] private int _quantityToCraft;

    private int _numCrafted = 0;

    private void OnEnable()
    {
        GameEventsManager.instance.craftEvents.onItemCrafted += ItemCrafted;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.craftEvents.onItemCrafted -= ItemCrafted;
    }

    private void ItemCrafted(string productCraftedId, int quantityCrafted)
    {
        if (_craftItem.IdItem == productCraftedId)
        {
            _numCrafted += quantityCrafted;
            SaveSegmentState();
        }

        if (_numCrafted >= _quantityToCraft)
        {
            FinishQuestSegment();
        }
    }

    protected override void LoadSegmentState(string segmentState)
    {
        if (int.TryParse(segmentState, out int result))
        {
            _numCrafted = result;
        }
        else
        {
            Debug.LogWarning("The state saved in the quest: " + _questId +
                " and segment: " + _segmentIndex + " is not a int.");
        }
    }

    protected override void SaveSegmentState()
    {
        string state = _numCrafted.ToString();
        ChangeState(new QuestSegmentState(state));
    }
}
