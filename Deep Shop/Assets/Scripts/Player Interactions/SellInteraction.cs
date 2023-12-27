using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellInteraction : MonoBehaviour
{
    [SerializeField]
    private TradeUI _tradeUIManager;

    private Rigidbody2D _rb;
    private GameObject _customer = null;
    private bool _isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayer && _customer && Input.GetKeyDown(KeyCode.E))
        {
            // open dialog
            _tradeUIManager.OpenTrade(_customer.GetComponent<CustomerTastes>().ProductDesired);
        }
    }

    public void EndTrade()
    {
        _customer = null;
        CustomerManager.instance.ExitStore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_customer == null && collision.gameObject.CompareTag("Customer"))
        {
            _customer = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _customer)
        {
            _customer = null;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }
}
