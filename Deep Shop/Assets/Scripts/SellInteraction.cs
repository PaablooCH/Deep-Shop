using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellInteraction : MonoBehaviour
{
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
        if (isPlayer && customer && Input.GetKey(KeyCode.E))
        {
            // open dialog
            Destroy(customer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
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
        Debug.Log("OnCollisionExit2D");
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
