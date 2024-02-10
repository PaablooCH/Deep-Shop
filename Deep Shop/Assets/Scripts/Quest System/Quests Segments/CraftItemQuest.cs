using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftItemQuest : QuestSegment
{
    [SerializeField] private ItemInfoSO _craftItem;
    [SerializeField] private int _quantityToCraft;

    private int _numCrafted = 0;

    private void Start()
    {
        // Set tooltip Image and Text
        Image itemImage = _questTooltipPrefab.GetComponentInChildren<Image>();
        itemImage.sprite = _craftItem.Sprite;
        itemImage.color = _craftItem.Color;

        UpdateTooltip();
    }

    private void OnEnable()
    {
        GameEventsMediator.instance.craftEvents.onItemCrafted += ItemCrafted;
    }

    private void OnDisable()
    {
        GameEventsMediator.instance.craftEvents.onItemCrafted -= ItemCrafted;
    }

    private void ItemCrafted(string productCraftedId, int quantityCrafted)
    {
        if (_craftItem.IdItem == productCraftedId)
        {
            _numCrafted += quantityCrafted;
            SaveSegmentState();
            UpdateTooltip();
        }

        if (_numCrafted >= _quantityToCraft)
        {
            FinishQuestSegment();
        }
    }

    protected override void UpdateTooltip()
    {
        TextMeshProUGUI craftText = _questTooltipPrefab.GetComponentInChildren<TextMeshProUGUI>();
        craftText.text = "Crafted " + _numCrafted + " of " + _quantityToCraft + " " + _craftItem.NameItem;
        if (_quantityToCraft > 1)
        {
            craftText.text += "s";
        }

        GameEventsMediator.instance.questEvents.UpdateQuestTooltip(_questId, _questTooltipPrefab);
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
