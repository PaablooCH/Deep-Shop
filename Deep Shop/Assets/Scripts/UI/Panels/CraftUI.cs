using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private ManageCraftGrid _manageCraftGrid;
    [SerializeField] private GameObject _prefabRecipeTooltip;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void CreateGrid()
    {
        foreach(Recipe recipe in RecipeManager.instance.RecipeMap.Values)
        {
            GameObject gridCraft = _manageCraftGrid.AddItem(recipe.GetResultItemId());
            _manageCraftGrid.ModifyQuantity(recipe.GetResultItemId(), recipe.GetResultQuantity());
            gridCraft.GetComponent<CraftAction>().RecipeId = recipe.GetRecipeId();
            CreateTooltip(recipe, gridCraft);
        }
    }

    private void CreateTooltip(Recipe recipe, GameObject gridCraft)
    {
        Item item = ItemsManager.instance.GetItemByID(recipe.GetResultItemId());

        TooltipGameObjectTrigger tooltipGameObjectTrigger = gridCraft.GetComponent<TooltipGameObjectTrigger>();
        tooltipGameObjectTrigger.Header = item.ItemInfo.NameItem;
        tooltipGameObjectTrigger.Body = item.ItemInfo.Description;
        int requireMoney = recipe.RecipeInfo.Money > 0 ? 1 : 0;
        GameObject[] gameObjectsTooltip = new GameObject[recipe.RecipeInfo.ProductsNeeded.Length + requireMoney];
        for (int i = 0; i < recipe.RecipeInfo.ProductsNeeded.Length; i++)
        {
            ItemQuantitySerialized productQuantity = recipe.RecipeInfo.ProductsNeeded[i];

            Item itemNeeded = ItemsManager.instance.GetItemByID(productQuantity.itemInfo.IdItem);
            string nameItem = itemNeeded.ItemInfo.NameItem;

            GameObject tooltipInfo = Instantiate(_prefabRecipeTooltip, gridCraft.transform);
            tooltipInfo.SetActive(false);

            tooltipInfo.transform.Find("Product Image").GetComponent<Image>().sprite =
                itemNeeded.ItemInfo.Sprite;

            TextMeshProUGUI textMeshPro = tooltipInfo.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMeshPro.text = productQuantity.quantity.ToString() + " x " + nameItem;

            // If not available ingredient print red
            if (InventoryManager.instance.GetInventory(productQuantity.itemInfo.IdItem) < productQuantity.quantity) 
            {
                textMeshPro.color = Color.red;
            }

            gameObjectsTooltip[i] = tooltipInfo;
        }

        if (recipe.RecipeInfo.Money > 0)
        {
            GameObject moneyNeeded = Instantiate(_prefabRecipeTooltip, gridCraft.transform);
            moneyNeeded.SetActive(false);

            moneyNeeded.transform.Find("Product Image").GetComponent<Image>().sprite =
                UtilsLoadResource.LoadSprite("Sprites/coin");

            TextMeshProUGUI textMeshPro = moneyNeeded.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMeshPro.text = recipe.RecipeInfo.Money + " G";

            // If not enough money print red
            if (InventoryManager.instance.Money < recipe.RecipeInfo.Money)
            {
                textMeshPro.color = Color.red;
            }

            gameObjectsTooltip[gameObjectsTooltip.Length - 1] = moneyNeeded;
        }
        tooltipGameObjectTrigger.GameObjectsTooltip = gameObjectsTooltip;
    }
}
