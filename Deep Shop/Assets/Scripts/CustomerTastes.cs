using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerType
{
    LEGAL,
    NOT_LEGAL
}

public class CustomerTastes : MonoBehaviour
{
    private GameObject productDesired;
    private CustomerType customerType;

    public GameObject ProductDesired { get => productDesired; set => productDesired = value; }
    public CustomerType CustomerType { get => customerType; set => customerType = value; }
}
