using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    LEGAL,
    NOT_LEGAL
}

public class Product : MonoBehaviour
{
    [SerializeField]
    [Min(0.1f)]
    private float buyPrice = 0.1f;
    
    [SerializeField]
    [Min(0.1f)]
    private float maxSoldPrice = 0.1f;

    [SerializeField]
    [Range(0.1f, 100f)]
    private float karma = 0.1f;

    [SerializeField] 
    private ProductType productType;

    public float BuyPrice { get => buyPrice; }
    public float MaxSoldPrice { get => maxSoldPrice; }
    public float Karma { get => karma; }
    public ProductType ProductType { get => productType; }
}
