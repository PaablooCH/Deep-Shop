using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftUI : MonoBehaviour
{
    [SerializeField]
    private ManageCraftGrid _manageCraftGrid;
    [SerializeField]
    private GameObject _prefabRecipeTooltip;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void CreateGrid()
    {
        foreach(Recipe recipe in RecipeManager.instance.Recipes)
        {
            GameObject productResult = ProductsManager.instance.SearchProductByID(recipe.productResult.idProduct);
            GameObject gridCraft = _manageCraftGrid.AddItem(productResult);
            _manageCraftGrid.ModifyQuantity(recipe.productResult.idProduct, recipe.productResult.quantity);
            gridCraft.GetComponent<CraftAction>().RecipeId = recipe.id;
            CreateTooltip(recipe, gridCraft);
        }
    }

    private void CreateTooltip(Recipe recipe, GameObject gridCraft)
    {
        ProductInfo productInfo = ProductsManager.instance.GetProductInfo(recipe.productResult.idProduct);

        TooltipGameObjectTrigger tooltipGameObjectTrigger = gridCraft.GetComponent<TooltipGameObjectTrigger>();
        tooltipGameObjectTrigger.Header = productInfo.Product.productName;
        tooltipGameObjectTrigger.Body = productInfo.Product.description;
        int requireMoney = recipe.money > 0 ? 1 : 0;
        GameObject[] gameObjectsTooltip = new GameObject[recipe.productsNeeded.Length + requireMoney];
        for (int i = 0; i < recipe.productsNeeded.Length; i++)
        {
            ProductQuantity productQuantity = recipe.productsNeeded[i];

            GameObject productNeeded = ProductsManager.instance.SearchProductByID(productQuantity.idProduct);
            string productName = productNeeded.GetComponent<ProductInfo>().Product.productName;

            GameObject tooltipInfo = Instantiate(_prefabRecipeTooltip, gridCraft.transform);
            tooltipInfo.SetActive(false);

            tooltipInfo.transform.Find("Product Image").GetComponent<Image>().sprite =
                productNeeded.GetComponent<SpriteRenderer>().sprite;

            TextMeshProUGUI textMeshPro = tooltipInfo.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMeshPro.text = productQuantity.quantity.ToString() + " x " + productName;

            if (InventoryManager.instance.GetInventory(productQuantity.idProduct) < productQuantity.quantity) // Not available ingredient
            {
                textMeshPro.color = Color.red;
            }

            gameObjectsTooltip[i] = tooltipInfo;
        }

        if (recipe.money > 0)
        {
            GameObject moneyNeeded = Instantiate(_prefabRecipeTooltip, gridCraft.transform);
            moneyNeeded.SetActive(false);

            moneyNeeded.transform.Find("Product Image").GetComponent<Image>().sprite =
                UtilsLoadResource.LoadSprite("Sprites/coin");

            TextMeshProUGUI textMeshPro = moneyNeeded.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMeshPro.text = recipe.money + " G";

            if (PlayerStats.instance.Money < recipe.money) // Not enough money
            {
                textMeshPro.color = Color.red;
            }

            gameObjectsTooltip[gameObjectsTooltip.Length - 1] = moneyNeeded;
        }
        tooltipGameObjectTrigger.GameObjectsTooltip = gameObjectsTooltip;
    }
}
