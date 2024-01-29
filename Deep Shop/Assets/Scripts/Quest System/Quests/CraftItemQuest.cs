using UnityEngine;

public class CraftItemQuest : QuestSegment
{
    [SerializeField] private int _craftItemId;
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

    private void ItemCrafted(int productCraftedId, int quantityCrafted)
    {
        if (_craftItemId == productCraftedId)
        {
            _numCrafted += quantityCrafted;   
        }

        if (_numCrafted >= _quantityToCraft)
        {
            FinishQuestSegment();
        }
    }
}
