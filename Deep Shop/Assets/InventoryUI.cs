using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool _childsInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.AddUI(UIType.INVENTORY, transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_childsInitialized)
        {
            UIManager.instance.FreeUI(UIType.INVENTORY);
            _childsInitialized = true;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            UIManager.instance.ActiveInventory();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.instance.FreeUI(UIType.INVENTORY);
        }
    }
}
