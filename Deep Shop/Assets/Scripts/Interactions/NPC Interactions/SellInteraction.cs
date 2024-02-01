using UnityEngine;

public class SellInteraction : PlayerNPCInteraction
{
    public override void EndInteraction()
    {
        base.EndInteraction();
        CustomerManager.instance.ExitStore();
    }

    protected override void CheckNPC(GameObject npc)
    {
        if (npc.CompareTag("Customer"))
        {
            _npc = npc;
        }
    }

    public override void Interact()
    {
        CanvasManager.instance.GetPanel(UIs.TRADE).GetComponent<TradeUI>()
                .OpenUI(_npc.GetComponent<CustomerTastes>().ItemDesired);
    }
}
