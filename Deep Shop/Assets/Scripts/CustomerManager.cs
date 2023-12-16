using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 5f;
    
    [SerializeField]
    private GameObject positionCorner; // probably only need transform
    [SerializeField]
    private GameObject positionStart; // probably only need transform
    [SerializeField]
    private GameObject positionExit; // probably only need transform

    [SerializeField]
    private GameObject circleCostumer;

    [SerializeField]
    private GameObject productLegal1;
    [SerializeField]
    private GameObject productLegal2;
    [SerializeField]
    private GameObject productLegal3;
    [SerializeField]
    private GameObject productNotLegal1;
    [SerializeField]
    private GameObject productNotLegal2;
    [SerializeField]
    private GameObject productNotLegal3;

    private Queue<GameObject> customers = new Queue<GameObject>();
    private float spawnCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (customers.Count < 4)
        {
            spawnCounter += Time.deltaTime;
            if (spawnTime <= spawnCounter)
            {
                InstantiateCustomer();
                spawnCounter = 0f;
            }
        }
    }

    public GameObject GetPositionCorner()
    {
        return positionCorner;
    }

    public GameObject GetPositionStart()
    {
        return positionStart;
    }

    public Transform NextPosition(Transform currentPosition)
    {
        // TODO return a new position different from the current
        return transform;
    }

    public void ExitStore()
    {
        GameObject customerServed = customers.Dequeue();
        customerServed.GetComponent<CustomerBehaviour>().ExitStore(positionExit.transform);
        if (customers.Count > 0)
        {
            GameObject customerToAttend = customers.Peek();
            customerToAttend.SetActive(true);
            customerToAttend.GetComponent<CustomerBehaviour>().SetTravelPoint(positionCorner.transform);
        }
    }

    private void InstantiateCustomer()
    {
        GameObject go = Instantiate(circleCostumer, positionStart.transform.position, circleCostumer.transform.rotation);
        CustomerTastes tastes = go.GetComponent<CustomerTastes>();
        tastes.ProductDesired = RandomProduct();
        customers.Enqueue(go);
        if (customers.Count == 1)
        {
            CustomerBehaviour cb = go.GetComponent<CustomerBehaviour>();
            cb.SetTravelPoint(positionCorner.transform);
        }
        else
        {
            go.SetActive(false);
        }
    }

    private GameObject RandomProduct()
    {
        int random = Random.Range(0, 100); // [0, 100)
        if (random < 35) // 35%
        {
            return productLegal1;
        }
        else if (random < 60) // 25%
        {
            return productLegal2;
        }
        else if (random < 75) // 15%
        {
            return productLegal3;
        }
        else if (random < 87) // 12%
        {
            return productNotLegal1;
        }
        else if (random < 95) // 8%
        {
            return productNotLegal2;
        }
        else // 4%
        {
            return productNotLegal3;
        }
    }
}
