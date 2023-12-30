using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyInteraction : PlayerNPCInteraction
{
    [SerializeField]
    private ShopUI _shopUI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayer && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            _shopUI.OpenTrade();
        }
    }

    public void EndTrade()
    {
        _npc = null;
        CustomerManager.instance.ExitStore();
    }

    protected override void NeededByInheritClasses(GameObject npc)
    {
        if (npc.CompareTag("Vendor"))
        {
            _npc = npc;
        }
    }
}
