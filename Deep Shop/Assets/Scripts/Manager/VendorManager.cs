using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorItem
{
    public int idProduct;
    public int quantity;

    public VendorItem(int idProduct, int quantity)
    {
        this.idProduct = idProduct;
        this.quantity = quantity;
    }
}

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
    private GameObject _positionCorner; // probably only need transform
    [SerializeField]
    private GameObject _positionStart; // probably only need transform
    [SerializeField]
    private GameObject _positionExit; // probably only need transform

    [SerializeField]
    private GameObject _basePrefabVendor;

    private GameObject _customers;
    private float _spawnCounter = 0f;

    private VendorItem[] _vendorItems;

    public VendorItem[] VendorItems { get => _vendorItems; set => _vendorItems = value; }

    // Start is called before the first frame update
    void Start()
    {
        _vendorItems = new VendorItem[] { new VendorItem(1, 2) };
        //InstantiateCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        //_spawnCounter += Time.deltaTime;
        //if (_spawnTime <= _spawnCounter)
        //{
        //    InstantiateCustomer();
        //    _spawnCounter = 0f;
        //}
    }

    public GameObject GetPositionCorner()
    {
        return _positionCorner;
    }

    public GameObject GetPositionStart()
    {
        return _positionStart;
    }

    public Transform NextPosition(Transform currentPosition)
    {
        // TODO return a new position different from the current
        return transform;
    }

    public void ExitStore()
    {

    }

    private void InstantiateCustomer()
    {
        GameObject go = Instantiate(_basePrefabVendor, _positionStart.transform.position, _basePrefabVendor.transform.rotation);
        CustomerTastes tastes = go.GetComponent<CustomerTastes>();
        tastes.ProductDesired = ProductsManager.instance.RandomProduct();
            CustomerBehaviour cb = go.GetComponent<CustomerBehaviour>();
            cb.SetTravelPoint(_positionCorner.transform);
    }
}
