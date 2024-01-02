using System.Collections;
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
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    //TODO do the class

    [SerializeField]
    private float _spawnTime = 5f;
    [SerializeField]
    private int _numberToSell = 3;

    [SerializeField]
    private GameObject _positionCorner; // probably only need transform
    [SerializeField]
    private GameObject _positionStart; // probably only need transform
    [SerializeField]
    private GameObject _positionExit; // probably only need transform

    [SerializeField]
    private GameObject _basePrefabVendor;

    private GameObject _actualVendor;

    private float _spawnCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateVendor();
    }

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
        VendorProductsToSell productsToSell = go.GetComponent<VendorProductsToSell>();
        if (productsToSell)
        {
            int numberToSell = _numberToSell < ProductsManager.instance.Products.Length ?
            _numberToSell : ProductsManager.instance.Products.Length;
            int howMany = UtilsNumberGenerator.GenerateNumberWithWeight(1, numberToSell, 2, 1);
            List<int> products = new();
            int i = 0;
            while (i < howMany)
            {
                int id = ProductsManager.instance.RandomProductID();
                if (!products.Contains(id))
                {
                    products.Add(id);
                    i++;
                }
            }

            VendorProduct[] vendorProducts = new VendorProduct[howMany];
            for (i = 0; i < howMany; i++)
            {
                int quantity = UtilsNumberGenerator.GenerateNumberWithWeight(1, 5, 3, 1);
                vendorProducts[i] = new VendorProduct(products[i], quantity);
            }
            productsToSell.VendorProducts = vendorProducts;
        }
        else
        {
            Debug.LogWarning("VendorProductsToSell doesn't exist in the gameObject");
        }
        
    }
}
