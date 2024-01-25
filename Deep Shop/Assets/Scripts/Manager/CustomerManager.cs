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

    [SerializeField] private float _spawnTime = 5f;
    
    [SerializeField] private GameObject _positionCorner; // probably only need transform
    [SerializeField] private GameObject _positionStart; // probably only need transform
    [SerializeField] private GameObject _positionExit; // probably only need transform

    [SerializeField] private GameObject _basePrefabCostumer;

    private Queue<GameObject> _customers = new();
    private float _spawnCounter = 0f;

    // Update is called once per frame
    void Update()
    {
        if (_customers.Count < 4)
        {
            _spawnCounter += Time.deltaTime;
            if (_spawnTime <= _spawnCounter)
            {
                InstantiateCustomer();
                _spawnCounter = 0f;
            }
        }
    }

    public Transform NextPosition(Transform currentPosition)
    {
        // TODO return a new position different from the current
        return transform;
    }

    public void ExitStore()
    {
        if (_customers.Count > 0)
        {
            GameObject customerServed = _customers.Dequeue();
            customerServed.GetComponent<NPCBehaviour>().ExitStore(_positionExit.transform);
        }
        if (_customers.Count > 0)
        {
            GameObject customerToAttend = _customers.Peek();
            customerToAttend.SetActive(true);
            customerToAttend.GetComponent<NPCBehaviour>().TravelPoint = _positionCorner.transform;
        }
    }

    private void InstantiateCustomer()
    {
        GameObject go = Instantiate(_basePrefabCostumer, _positionStart.transform.position, _basePrefabCostumer.transform.rotation);
        CustomerTastes tastes = go.GetComponent<CustomerTastes>();
        tastes.ProductDesired = ProductsManager.instance.RandomProduct();
        _customers.Enqueue(go);
        if (_customers.Count == 1)
        {
            NPCBehaviour npcBehaviour = go.GetComponent<NPCBehaviour>();
            if (npcBehaviour)
            {
                npcBehaviour.GoToPoint(_positionCorner.transform);
            }
            else
            {
                Debug.LogWarning("NPCBehaviour doesn't exist in the gameObject");
            }
        }
        else
        {
            go.SetActive(false);
        }
    }
}
