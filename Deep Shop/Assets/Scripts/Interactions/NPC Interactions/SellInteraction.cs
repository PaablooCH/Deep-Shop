using UnityEngine;

public class SellInteraction : PlayerNPCInteraction
{
    [SerializeField]
    private TradeUI _tradeUIManager;

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
            _tradeUIManager.OpenUI(_npc.GetComponent<CustomerTastes>().ProductDesired);
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
