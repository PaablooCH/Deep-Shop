using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CraftAction : MonoBehaviour, IPointerEnterHandler
{
    private TooltipGameObjectTrigger _tooltipGameObject;
    private int _recipeId;

    public int RecipeId { get => _recipeId; set => _recipeId = value; }

    private void Start()
    {
        _tooltipGameObject = GetComponent<TooltipGameObjectTrigger>();
    }

    public void ClickButtonCraft()
    {
        Recipe recipe = RecipeManager.instance.SearchRecipeByID(_recipeId);
        bool canCraft = true;
        for (int i = 0; i < recipe.productsNeeded.Length; i++)
        {
            ProductQuantity productNeeded = recipe.productsNeeded[i];
            int inventoryQuantity = InventoryManager.instance.GetInventory(productNeeded.idProduct);
            if (inventoryQuantity < productNeeded.quantity)
            {
                canCraft = false;
                break;
            }
        }
        if (canCraft)
        {
            CraftItem();
        }
    }

    private void CraftItem()
    {
        Recipe recipe = RecipeManager.instance.SearchRecipeByID(_recipeId);

        for (int i = 0; i < recipe.productsNeeded.Length; i++)
        {
            ProductQuantity productNeeded = recipe.productsNeeded[i];

            InventoryManager.instance.ModifyInventory(productNeeded.idProduct, -productNeeded.quantity);

            int inventoryQuantity = InventoryManager.instance.GetInventory(productNeeded.idProduct);
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

        InventoryManager.instance.ModifyInventory(recipe.productResult.idProduct, recipe.productResult.quantity);
        _tooltipGameObject.ResetGameObjects();
    }

    public void OnPointerEnter(PointerEventData eventData) // I move this script above TooltipGameObjectTrigger
                                                           // to make sure this is called before.
    {
        Recipe recipe = RecipeManager.instance.SearchRecipeByID(_recipeId);
        for (int i = 0; i < recipe.productsNeeded.Length; i++)
        {
            ProductQuantity productNeeded = recipe.productsNeeded[i];
            int inventoryQuantity = InventoryManager.instance.GetInventory(productNeeded.idProduct);
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
}
