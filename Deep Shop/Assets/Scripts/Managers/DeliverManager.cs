using System.Collections.Generic;
using UnityEngine;

public class DeliverObject
{
    private float _timeToDeliver;
    private ItemQuantity _productQuantity;

    public DeliverObject(float timeToDeliver, ItemQuantity productQuantity)
    {
        _timeToDeliver = timeToDeliver;
        _productQuantity = productQuantity;
    }

    public float TimeToDeliver { get => _timeToDeliver; set => _timeToDeliver = value; }
    public ItemQuantity ProductQuantity { get => _productQuantity; set => _productQuantity = value; }
}

public class DeliverManager : MonoBehaviour
{
    #region Singleton
    public static DeliverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Deliver Manager singleton already exists.");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private PackageWithItems _deliveryPlace;

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
