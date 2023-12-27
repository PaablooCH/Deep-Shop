using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public int id;
    public string name;
    public float buyPrice;
    public float maxSoldPrice;
    public float karma;
    public int weightSpawn;
    public string spritePath;
    public MyColor color;
}

public class ProductInfo : MonoBehaviour
{
    private Product _product;

    public Product product { get => _product; set => _product = value; }

    public float CalculateKarma(float priceUnit)
    {
        float percentatge = CalculatePercentatgeBuy(priceUnit);
        if (priceUnit < _product.buyPrice) // example 0.75
        {
            percentatge -= 1; // 0.25
            return _product.karma * (1 + percentatge); // karma * 1.25
        }
        if (priceUnit <= _product.buyPrice + _product.maxSoldPrice)
        {
            return _product.karma;
        }
        if (percentatge < 2) // example 1.4
        {
            percentatge = 1 - (percentatge - (int) percentatge); // 1 - 0.4 -> 0.6
            return _product.karma * percentatge; // karma * 0.6
        }
        else
        {
            return -_product.karma * 1.5f;
        }
    }

    public float CalculatePercentatgeBuy(float priceTrade)
    {
        return priceTrade / _product.buyPrice;
    }
}
