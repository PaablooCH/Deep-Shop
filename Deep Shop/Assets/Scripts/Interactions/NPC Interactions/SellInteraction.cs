using UnityEngine;

public class SellInteraction : PlayerNPCInteraction
{
    // Update is called once per frame
    void Update()
    {
        if (_isPlayer && _npc && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            CanvasManager.instance.GetPanel(UIs.TRADE).GetComponent<TradeUI>()
                .OpenUI(_npc.GetComponent<CustomerTastes>().ItemDesired);
        }
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
        CustomerManager.instance.ExitStore();
    }

    protected override void NeededByInheritClasses(GameObject npc)
    {
        if (npc.CompareTag("Customer"))
        {
            _npc = npc;
        }
    }
}
