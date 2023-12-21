using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    #region Singleton
    public static CustomerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField]
    private float spawnTime = 5f;
    
    [SerializeField]
    private GameObject positionCorner; // probably only need transform
    [SerializeField]
    private GameObject positionStart; // probably only need transform
    [SerializeField]
    private GameObject positionExit; // probably only need transform

    [SerializeField]
    private GameObject basePrefabCostumer;

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
        if (customers.Count > 0)
        {
            GameObject customerServed = customers.Dequeue();
            customerServed.GetComponent<CustomerBehaviour>().ExitStore(positionExit.transform);
        }
        if (customers.Count > 0)
        {
            GameObject customerToAttend = customers.Peek();
            customerToAttend.SetActive(true);
            customerToAttend.GetComponent<CustomerBehaviour>().SetTravelPoint(positionCorner.transform);
        }
    }

    private void InstantiateCustomer()
    {
        GameObject go = Instantiate(basePrefabCostumer, positionStart.transform.position, basePrefabCostumer.transform.rotation);
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
            return ProductsManager.instance.SearchProduct(ProductType.LEGAL_1);
        }
        else if (random < 60) // 25%
        {
            return ProductsManager.instance.SearchProduct(ProductType.LEGAL_2);
        }
        else if (random < 75) // 15%
        {
            return ProductsManager.instance.SearchProduct(ProductType.LEGAL_3);
        }
        else if (random < 87) // 12%
        {
            return ProductsManager.instance.SearchProduct(ProductType.NOT_LEGAL_1);
        }
        else if (random < 95) // 8%
        {
            return ProductsManager.instance.SearchProduct(ProductType.NOT_LEGAL_2);
        }
        else // 4%
        {
            return ProductsManager.instance.SearchProduct(ProductType.NOT_LEGAL_3);
        }
    }
}
