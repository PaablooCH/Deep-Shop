using UnityEngine;

public enum CustomerType
{
    LEGAL,
    NOT_LEGAL
}

public class CustomerTastes : MonoBehaviour
{
    private GameObject _productDesired;
    private CustomerType _customerType;

    public GameObject ProductDesired { get => _productDesired; set => _productDesired = value; }
}
