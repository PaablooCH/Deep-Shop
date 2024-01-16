using System.Collections.Generic;
using UnityEngine;

public class DeliverObject
{
    private float _timeToDeliver;
    private ProductQuantity _productQuantity;

    public DeliverObject(float timeToDeliver, ProductQuantity vendorProduct)
    {
        _timeToDeliver = timeToDeliver;
        _productQuantity = vendorProduct;
    }

    public float TimeToDeliver { get => _timeToDeliver; set => _timeToDeliver = value; }
    public ProductQuantity ProductQuantity { get => _productQuantity; set => _productQuantity = value; }
}

public class DeliverManager : MonoBehaviour
{
    #region Singleton
    public static DeliverManager instance;

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
    private PackageWithItems _deliveryPlace;

    private List<DeliverObject> _packages = new();

    public List<DeliverObject> Packages { get => _packages; set => _packages = value; }

    // Update is called once per frame
    void Update()
    {
        List<DeliverObject> auxList = new();
        foreach (DeliverObject deliverObject in _packages)
        {
            deliverObject.TimeToDeliver -= Time.deltaTime;
            if (deliverObject.TimeToDeliver <= 0f)
            {
                _deliveryPlace.AddNewPackage(deliverObject.ProductQuantity);
            }
            else
            {
                auxList.Add(deliverObject);
            }
        }
        _packages = auxList;
    }
}
