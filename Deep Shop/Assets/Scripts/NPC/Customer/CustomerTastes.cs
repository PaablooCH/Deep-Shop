using UnityEngine;

public enum CustomerType
{
    LEGAL,
    NOT_LEGAL
}

public class CustomerTastes : MonoBehaviour
{
    private Item _itemDesired;
    private CustomerType _customerType;

    public Item ItemDesired { get => _itemDesired; set => _itemDesired = value; }
}
