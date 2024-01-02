using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, IUI
{
    private bool _childsInitialized = false;

    public void OpenUI(GameObject go)
    {
        CanvasManager.instance.ActiveInventory();
    }

    public void Exit()
    {
        CanvasManager.instance.FreeUI(UIType.INVENTORY);
    }

    // Start is called before the first frame update
    void Start()
    {
        CanvasManager.instance.AddUI(UIType.INVENTORY, transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_childsInitialized)
        {
            CanvasManager.instance.FreeUI(UIType.INVENTORY);
            _childsInitialized = true;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            OpenUI(null);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Exit();
        }
    }
}
