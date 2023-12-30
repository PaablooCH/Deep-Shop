using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    protected List<int> productsInGrid = new();     // I store the idPRoduct,
                                                    // the index in the list is a reference of child position
                                                    // in transform

    public void AddItem(GameObject newItem)
    {
        int newId = newItem.GetComponent<ProductInfo>().Product.id;
        if (!productsInGrid.Contains(newId))
        {
            productsInGrid.Add(newId);
            GameObject gridObject = Instantiate(basePrefab, gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
        }
    }

    public void ModifyQuantity(int modifiedItem, int amount)
    {
        int index = productsInGrid.FindIndex((idProduct) => idProduct == modifiedItem);
        TextMeshProUGUI text = gridTransform.GetChild(index).transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        text.text = amount.ToString();
    }

    protected void RemoveItem(int removedItem)
    {
        int index = productsInGrid.FindIndex((productType) => productType == removedItem);
        Destroy(gridTransform.GetChild(index));
        productsInGrid.Remove(removedItem);
    }

    public int GetIdProductFromChild(int siblingPosition)
    {
        if (siblingPosition < gridTransform.childCount)
        {
            return productsInGrid[siblingPosition];
        }
        return -1;
    }
}
