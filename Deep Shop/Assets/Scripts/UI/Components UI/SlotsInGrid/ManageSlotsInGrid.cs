using System.Collections.Generic;
using UnityEngine;

public abstract class ManageSlotsInGrid : MonoBehaviour
{
#pragma warning disable 0414
    // Remember to use the correct derived class
    [ReadOnly]
    [SerializeField] private string remember = "Remember to use the correct derived class";
#pragma warning restore 0414

    [SerializeField] protected Transform _gridTransform;
    [SerializeField] protected GameObject _slotPrefab;

    protected Dictionary<string, GameObject> _productsInGrid = new(); // key -> itemID; value -> slot

    public abstract GameObject AddItem(string newItemId);

    protected void RemoveItem(string removedItem)
    {
        if (_productsInGrid.TryGetValue(removedItem, out GameObject slot))
        {
            Destroy(slot);
        }
    }

    public void CleanGrid()
    {
        _productsInGrid.Clear();
        for (int i = _gridTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = _gridTransform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
