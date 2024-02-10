using UnityEngine;

public class SellInteraction : PlayerNPCInteraction
{
    public override void EndInteraction()
    {
        _npc.GetComponent<NPCBehaviour>().ExitStore();
        base.EndInteraction();
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
        UIManager.instance.OpenUI(UIs.TRADE, _npc.GetComponent<CustomerTastes>().ItemDesired);
    }
}
