using System.Collections.Generic;
using UnityEngine;

public class VendorManager : MonoBehaviour
{
    #region Singleton
    public static VendorManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Vendor Manager singleton already exists."); 
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private float _spawnTime = 5f;

    [SerializeField] private int _numberToSell = 3;

    [SerializeField] private GameObject _positionCorner; // probably only need transform
    [SerializeField] private GameObject _positionStart; // probably only need transform
    [SerializeField] private GameObject _positionExit; // probably only need transform

    [SerializeField] private GameObject _basePrefabVendor;

    private GameObject _actualVendor;

    private float _spawnCounter = 0f;

    // Update is called once per frame
    void Update()
    {
        if (_actualVendor == null)
        {
            _spawnCounter += Time.deltaTime;
            if (_spawnTime <= _spawnCounter)
            {
                InstantiateVendor();
                _spawnCounter = 0f;
            }
        }
    }

    public void ExitStore()
    {
        if (_actualVendor)
        {
            _actualVendor.GetComponent<NPCBehaviour>().ExitStore(_positionExit.transform);
        }
    }

    private void InstantiateVendor()
    {
        _actualVendor = Instantiate(_basePrefabVendor, _positionStart.transform.position, _basePrefabVendor.transform.rotation);

        CreateVendorProducts(_actualVendor);

        NPCBehaviour npcBehaviour = _actualVendor.GetComponent<NPCBehaviour>();
        if (npcBehaviour)
        {
            npcBehaviour.GoToPoint(_positionCorner.transform);
        }
        else
        {
            Debug.LogWarning("NPCBehaviour doesn't exist in the gameObject");
        }
    }

    private void CreateVendorProducts(GameObject go)
    {
        if (go.TryGetComponent(out VendorItemToSell productsToSell))
        {
            int numberToSell = _numberToSell < ItemsManager.instance.HowManyItemsExist() ?
            _numberToSell : ItemsManager.instance.HowManyItemsExist();
            int howMany = UtilsNumberGenerator.GenerateNumberWithWeight(1, numberToSell, 2, 1);
            List<Item> products = new();
            int i = 0;
            while (i < howMany)
            {
                Item item = ItemsManager.instance.RandomItem();
                if (!products.Contains(item))
                {
                    products.Add(item);
                    i++;
                }
            }

            ItemQuantity[] vendorProducts = new ItemQuantity[howMany];
            for (i = 0; i < howMany; i++)
            {
                int quantity = UtilsNumberGenerator.GenerateNumberWithWeight(1, 5, 3, 1);
                vendorProducts[i] = new ItemQuantity(products[i], quantity);
            }
            productsToSell.VendorProducts = vendorProducts;
        }
        else
        {
            Debug.LogWarning("VendorProductsToSell doesn't exist in the gameObject");
        }
        
    }
}
