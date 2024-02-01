using UnityEngine;

public class BuyInteraction : PlayerNPCInteraction
{
    public override void EndInteraction()
    {
        base.EndInteraction();
        VendorManager.instance.ExitStore();
    }

    protected override void CheckNPC(GameObject npc)
    {
        if (npc.CompareTag("Vendor"))
        {
            _npc = npc;
        }
    }

    public override void Interact()
    {
        CanvasManager.instance.GetPanel(UIs.SHOP).GetComponent<ShopUI>().OpenUI(_npc);
    }
}
