using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 5f;
    
    [SerializeField] private Transform _positionCorner;
    [SerializeField] private Transform _positionStart;
    [SerializeField] private Transform _positionExit;

    [SerializeField] private GameObject _basePrefabCustomer;

    private Queue<GameObject> _customers = new();
    private float _spawnCounter = 0f;

    private void OnEnable()
    {
        GameEventsMediator.instance.npcEvents.onNPCExit += ExitStore;
    }

    private void OnDisable()
    {
        GameEventsMediator.instance.npcEvents.onNPCExit -= ExitStore;
    }

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

    private void ExitStore(string tag)
    {
        if (tag.Equals("Customer"))
        {
            _customers.Dequeue();
            // If we have the new first to the shop
            if (_customers.Count > 0)
            {
                GameObject customerToAttend = _customers.Peek();
                customerToAttend.SetActive(true);
                customerToAttend.GetComponent<NPCBehaviour>().TravelPoint = _positionCorner;
            }
        }
    }

    private void InstantiateCustomer()
    {
        // Instantiate a new Customer and give him an item
        GameObject go = Instantiate(_basePrefabCustomer, transform);
        CustomerTastes tastes = go.GetComponent<CustomerTastes>();
        tastes.ItemDesired = ItemsManager.instance.RandomItem();

        // Enqueue customer
        _customers.Enqueue(go);

        // If we only have 1 customer send him to the shop
        NPCBehaviour npcBehaviour = go.GetComponent<NPCBehaviour>();
        npcBehaviour.SetPositions(_positionCorner, _positionStart, _positionExit);
        if (_customers.Count != 1)
        {
            go.SetActive(false);
        }
    }
}
