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
        }

        if (_numCrafted >= _quantityToCraft)
        {
            FinishQuestSegment();
        }
    }
}
