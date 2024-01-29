using UnityEngine;

public class BuyInteraction : PlayerNPCInteraction
{
    // Update is called once per frame
    void Update()
    {
        if (_isPlayer && _npc && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            CanvasManager.instance.GetPanel(UIs.SHOP).GetComponent<ShopUI>().OpenUI(_npc);
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
