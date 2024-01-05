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
        if (_isPlayer && _npc && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            _shopUI.OpenUI(_npc);
        }
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
        VendorManager.instance.ExitStore();
    }

    protected override void NeededByInheritClasses(GameObject npc)
    {
        if (npc.CompareTag("Vendor"))
        {
            _npc = npc;
        }
    }
}
