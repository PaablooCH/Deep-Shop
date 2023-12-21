using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageObjectIntoGrid : MonoBehaviour
{
    [SerializeField]
    private Transform gridTransform;
    [SerializeField]
    private GameObject basePrefab;

    private List<GameObject> goInGrid = new();

    // We assume newItem has SpriteRender component
    // TODO change to SpriteRender
    public void AddItem(GameObject newItem, int amount)
    {
        goInGrid.Add(newItem);
        GameObject gridObject = Instantiate(basePrefab, gridTransform);
        SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
        Image image = gridObject.GetComponentInChildren<Image>();
        image.sprite = spriteRenderer.sprite;
        image.color = spriteRenderer.color;

        TextMeshProUGUI text = gridObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = amount.ToString();
    }

    public void RemoveItem(GameObject removedItem)
    {
        goInGrid.Remove(removedItem);
        
    }
}
