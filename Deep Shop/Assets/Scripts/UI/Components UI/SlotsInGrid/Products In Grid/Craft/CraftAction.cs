using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CraftAction : MonoBehaviour, IPointerEnterHandler
{
    private TooltipGameObjectTrigger _tooltipGameObject;
    private string _recipeId;

    public string RecipeId { get => _recipeId; set => _recipeId = value; }

    private void Start()
    {
        _tooltipGameObject = GetComponent<TooltipGameObjectTrigger>();
    }

    public void ClickButtonCraft()
    {
        Recipe recipe = RecipesManager.instance.SearchRecipeByID(_recipeId);
        
        bool canCraft = true;
        if (PlayerManager.instance.GetPlayerInventory().Money < recipe.RecipeInfo.Money)
        {
            canCraft = false;
        }
        else
        {
            for (int i = 0; i < recipe.RecipeInfo.ItemsNeeded.Length; i++)
            {
                ItemQuantitySerialized productNeeded = recipe.RecipeInfo.ItemsNeeded[i];
                int inventoryQuantity = PlayerManager.instance.GetPlayerInventory().GetInventory(productNeeded.itemInfo.IdItem);
                if (inventoryQuantity < productNeeded.quantity)
                {
                    canCraft = false;
                    break;
                }
            }
        }

        if (canCraft)
        {
            CraftItem();
        }
    }

    private void CraftItem()
    {
        Recipe recipe = RecipesManager.instance.SearchRecipeByID(_recipeId);

        for (int i = 0; i < recipe.RecipeInfo.ItemsNeeded.Length; i++)
        {
            ItemQuantitySerialized productNeeded = recipe.RecipeInfo.ItemsNeeded[i];

            PlayerManager.instance.GetPlayerInventory().ModifyInventory(productNeeded.itemInfo.IdItem, -productNeeded.quantity);

            UpdateProductTooltip(i, productNeeded);
        }

        // Money
        PlayerManager.instance.GetPlayerInventory().Money -= recipe.RecipeInfo.Money;

        UpdateMoneyTooltip(recipe);

        PlayerManager.instance.GetPlayerInventory().ModifyInventory(recipe.GetResultItemId(), recipe.GetResultQuantity());
        GameEventsMediator.instance.craftEvents.ItemCrafted(recipe.GetResultItemId(), recipe.GetResultQuantity());

        _tooltipGameObject.ResetGameObjects();
    }

    public void OnPointerEnter(PointerEventData eventData) // I move this script above TooltipGameObjectTrigger
                                                           // to make sure this is called before.
    {
        Recipe recipe = RecipesManager.instance.SearchRecipeByID(_recipeId);
        for (int i = 0; i < recipe.RecipeInfo.ItemsNeeded.Length; i++)
        {
            ItemQuantitySerialized productNeeded = recipe.RecipeInfo.ItemsNeeded[i];
            UpdateProductTooltip(i, productNeeded);
        }

        UpdateMoneyTooltip(recipe);
    }

    private void UpdateMoneyTooltip(Recipe recipe)
    {
        float moneyLeft = PlayerManager.instance.GetPlayerInventory().Money;
        GameObject moneyTooltip = _tooltipGameObject.GameObjectsTooltip[_tooltipGameObject.GameObjectsTooltip.Length - 1];
        if (moneyLeft < recipe.RecipeInfo.Money)
        {
            moneyTooltip.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            moneyTooltip.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }

    private void UpdateProductTooltip(int i, ItemQuantitySerialized productNeeded)
    {
        int inventoryQuantity = PlayerManager.instance.GetPlayerInventory().GetInventory(productNeeded.itemInfo.IdItem);
        GameObject tooltip = _tooltipGameObject.GameObjectsTooltip[i]; // Get Recipe Tool Tip Slot GameObject
        if (inventoryQuantity < productNeeded.quantity)
        {
            tooltip.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            tooltip.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }
}
