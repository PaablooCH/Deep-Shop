using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Transform gridTransform;
    [SerializeField]
    private GameObject basePrefab;
    
    private List<ProductType> goInGrid = new();

    private void Start()
    {
        Inventory.instance.onAddItem += AddItem;
        Inventory.instance.onModifyItem += ModifyItem;
        Inventory.instance.onRemoveItem += RemoveItem;
        Inventory.instance.StartItems();
    }

    private void AddItem(GameObject newItem)
    {
        ProductType newType = newItem.GetComponent<ProductInfo>().Product.productType;
        if (!goInGrid.Contains(newType))
        {
            goInGrid.Add(newType);
            GameObject gridObject = Instantiate(basePrefab, gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.GetComponentInChildren<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
        }
    }

    private void ModifyItem(ProductType modifiedItem, int amount)
    {
        int index = goInGrid.FindIndex((productType) => productType == modifiedItem);
        TextMeshProUGUI text = gridTransform.GetChild(index).GetComponentInChildren<TextMeshProUGUI>();
        text.text = amount.ToString();
    }

    private void RemoveItem(ProductType removedItem)
    {
        int index = goInGrid.FindIndex((productType) => productType == removedItem);
        Destroy(gridTransform.GetChild(index));
        goInGrid.Remove(removedItem);
    }
}
