using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSlotsInGrid : MonoBehaviour
{
#pragma warning disable 0414
    // Remember to use the correct derived class
    [ReadOnly]
    [SerializeField]
    private string remember = "Remember to use the correct derived class";
#pragma warning restore 0414

    [SerializeField]
    protected Transform _gridTransform;
    [SerializeField]
    protected GameObject _slotPrefab;

    protected List<int> _productsInGrid = new();    // I store the idPRoduct,
                                                    // the index in the list is a reference of child position
                                                    // in transform

    public virtual GameObject AddItem(GameObject newItem)
    {
        int newId = newItem.GetComponent<ProductInfo>().Product.id;
        if (!_productsInGrid.Contains(newId))
        {
            _productsInGrid.Add(newId);
            GameObject gridObject = Instantiate(_slotPrefab, _gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
            return gridObject;
        }
        int index = _productsInGrid.FindIndex((idProduct) => idProduct == newId);
        return _gridTransform.GetChild(index).gameObject;
    }

    public void ModifyQuantity(int modifiedItem, int amount)
    {
        int index = _productsInGrid.FindIndex((idProduct) => idProduct == modifiedItem);
        TextMeshProUGUI text = _gridTransform.GetChild(index).Find("Quantity").GetComponent<TextMeshProUGUI>();
        text.text = amount.ToString();
    }

    protected void RemoveItem(int removedItem)
    {
        int index = _productsInGrid.FindIndex((id) => id == removedItem);
        if (index != -1)
        {
            Destroy(_gridTransform.GetChild(index).gameObject);
            _productsInGrid.RemoveAt(index);
        }
    }

    public virtual void CleanGrid()
    {
        _productsInGrid.Clear();
        for (int i = _gridTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = _gridTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public int GetIdProductFromChild(int siblingPosition)
    {
        if (siblingPosition < _gridTransform.childCount)
        {
            return _productsInGrid[siblingPosition];
        }
        return -1;
    }
}
