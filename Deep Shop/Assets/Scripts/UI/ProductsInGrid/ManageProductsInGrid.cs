using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageProductsInGrid : MonoBehaviour
{
#pragma warning disable 0414
    // Remember to use the correct derived class
    [ReadOnly]
    [SerializeField]
    private string remember = "Remember to use the correct derived class";
#pragma warning restore 0414

    [SerializeField]
    protected Transform gridTransform;
    [SerializeField]
    protected GameObject basePrefab;

    protected List<ProductType> productsInGrid = new(); // the index in the list is a reference of child position
                                                        // in transform

    protected void AddItem(GameObject newItem)
    {
        ProductType newType = newItem.GetComponent<ProductInfo>().Product.productType;
        if (!productsInGrid.Contains(newType))
        {
            productsInGrid.Add(newType);
            GameObject gridObject = Instantiate(basePrefab, gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
        }
    }

    protected void RemoveItem(ProductType removedItem)
    {
        int index = productsInGrid.FindIndex((productType) => productType == removedItem);
        Destroy(gridTransform.GetChild(index));
        productsInGrid.Remove(removedItem);
    }

    public ProductType GetProductTypeFromChild(int siblingPosition)
    {
        if (siblingPosition < gridTransform.childCount)
        {
            return productsInGrid[siblingPosition];
        }
        return ProductType.NONE;
    }
}
