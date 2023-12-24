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
    NOT_LEGAL_3,
    NONE
}

[System.Serializable]
public class Product
{
    public float buyPrice;
    public float maxSoldPrice;
    public float karma;
    public ProductType productType;
    public string spritePath;
    public MyColor color;
}

public class ProductInfo : MonoBehaviour
{
    private Product product;

    public Product Product { get => product; set => product = value; }

    public float CalculateKarma(float priceUnit)
    {
        float percentatge = CalculatePercentatgeBuy(priceUnit);
        if (priceUnit < product.buyPrice) // example 0.75
        {
            percentatge -= 1; // 0.25
            return product.karma * (1 + percentatge); // karma * 1.25
        }
        if (priceUnit <= product.buyPrice + product.maxSoldPrice)
        {
            return product.karma;
        }
        if (percentatge < 2) // example 1.4
        {
            percentatge = 1 - (percentatge - (int) percentatge); // 1 - 0.4 -> 0.6
            return product.karma * percentatge; // karma * 0.6
        }
        else
        {
            return -product.karma * 1.5f;
        }
    }

    public float CalculatePercentatgeBuy(float priceTrade)
    {
        return priceTrade / product.buyPrice;
    }
}
