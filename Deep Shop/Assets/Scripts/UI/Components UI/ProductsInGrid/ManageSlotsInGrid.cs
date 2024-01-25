using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageSlotsInGrid : MonoBehaviour
{
#pragma warning disable 0414
    // Remember to use the correct derived class
    [ReadOnly]
    [SerializeField] private string remember = "Remember to use the correct derived class";
#pragma warning restore 0414

    [SerializeField] protected Transform _gridTransform;
    [SerializeField] protected GameObject _slotPrefab;

    protected Dictionary<int, GameObject> _productsInGrid = new(); // key -> productID; value -> slot

    public virtual GameObject AddItem(GameObject newItem)
    {
        int newId = newItem.GetComponent<ProductInfo>().Product.id;
        if (!_productsInGrid.ContainsKey(newId))
        {
            GameObject gridObject = Instantiate(_slotPrefab, _gridTransform);
            SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
            Image image = gridObject.transform.Find("Product Image").GetComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.enabled = true;
            _productsInGrid.Add(newId, gridObject);
            return gridObject;
        }
        return _productsInGrid[newId];
    }

    public void ModifyQuantity(int modifiedItem, int amount)
    {
        if (_productsInGrid.TryGetValue(modifiedItem, out GameObject slot))
        {
            TextMeshProUGUI text = slot.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            text.text = amount.ToString();
        }
    }

    protected void RemoveItem(int removedItem)
    {
        if (_productsInGrid.TryGetValue(removedItem, out GameObject slot))
        {
            Destroy(slot);
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
}
