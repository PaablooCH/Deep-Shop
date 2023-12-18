using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellInteraction : MonoBehaviour
{
    [SerializeField]
    private CustomerManager customerManager;
    [SerializeField]
    private TradeUIManager tradeUIManager;

    private Rigidbody2D rb;
    private GameObject customer = null;
    private bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer && customer && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            tradeUIManager.OpenTrade(customer.GetComponent<CustomerTastes>().ProductDesired);
        }
    }

    public void EndTrade()
    {
        customer = null;
        customerManager.ExitStore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (customer == null && collision.gameObject.CompareTag("Customer"))
        {
            customer = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == customer)
        {
            customer = null;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
