using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftUI : MonoBehaviour, IUI
{
    [SerializeField]
    private ManageCraftGrid _manageCraftGrid;
    [SerializeField]
    private GameObject _prefabRecipeTooltip;

    private bool _gridCreated = false;

    private void Start()
    {
        gameObject.SetActive(false);
        CanvasManager.instance.AddUI(UIType.CRAFT, gameObject);
    }

    public void Exit()
    {
        CanvasManager.instance.FreeUI(UIType.CRAFT);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateGrid();
        }
    }

    public void OpenUI(GameObject go)
    {
        if (!_gridCreated)
        {
            CreateGrid();
        }
        CanvasManager.instance.ActiveUI(UIType.CRAFT);
    }

    public void CreateGrid()
    {
        foreach(Recipe recipe in RecipeManager.instance.Recipes)
        {
            GameObject productResult = ProductsManager.instance.SearchProductByID(recipe.productResult.idProduct);
            GameObject gridCraft = _manageCraftGrid.AddItem(productResult);
            _manageCraftGrid.ModifyQuantity(recipe.productResult.idProduct, recipe.productResult.quantity);
            CreateTooltip(recipe, gridCraft);
        }
        _gridCreated = true;
    }

    private void CreateTooltip(Recipe recipe, GameObject gridCraft)
    {
        ProductInfo productInfo = ProductsManager.instance.GetProductInfo(recipe.productResult.idProduct);

        TooltipGameObjectTrigger tooltipGameObjectTrigger = gridCraft.GetComponent<TooltipGameObjectTrigger>();
        tooltipGameObjectTrigger.Header = productInfo.Product.productName;
        tooltipGameObjectTrigger.Body = productInfo.Product.description;
        GameObject[] gameObjectsTooltip = new GameObject[recipe.productsNeeded.Length];
        for (int i = 0; i < recipe.productsNeeded.Length; i++)
        {
            ProductQuantity productQuantity = recipe.productsNeeded[i];

            GameObject productNeeded = ProductsManager.instance.SearchProductByID(productQuantity.idProduct);
            string productName = productNeeded.GetComponent<ProductInfo>().Product.productName;

            GameObject tooltipInfo = Instantiate(_prefabRecipeTooltip, gridCraft.transform);
            tooltipInfo.SetActive(false);

            tooltipInfo.transform.Find("Product Image").GetComponent<Image>().sprite =
                productNeeded.GetComponent<SpriteRenderer>().sprite;

            tooltipInfo.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
                productQuantity.quantity.ToString() + " x " + productName;

            gameObjectsTooltip[i] = tooltipInfo;
        }
        tooltipGameObjectTrigger.GameObjectsTooltip = gameObjectsTooltip;
    }
}
