using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    LEGAL_1,
    LEGAL_2,
    LEGAL_3,
    NOT_LEGAL_1,
    NOT_LEGAL_2,
    NOT_LEGAL_3
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
    [Range(-100f, 100f)]
    private float karma = 0f;

    [SerializeField] 
    private ProductType productType;

    public float BuyPrice { get => buyPrice; }
    public float MaxSoldPrice { get => maxSoldPrice; }
    public float Karma { get => karma; }
    public ProductType ProductType { get => productType; }
}
