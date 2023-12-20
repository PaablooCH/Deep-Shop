using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductArray
{
    public Product[] products;
}

public class ProductsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject basePrefab;
    [SerializeField]
    private GameObject[] products;

    public GameObject[] Products { get => products; set => products = value; }

    private void Awake()
    {
        //ReadJSON();
    }

    private void Start()
    {
        ReadJSON();
    }

    public GameObject SearchProduct(ProductType productType)
    {
        foreach(GameObject go in products)
        {
            if (go.GetComponent<ProductInfo>().Product.productType == productType)
            {
                return go;
            }
        }
        return null;
    }

    private void ReadJSON()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("products");
        if (jsonAsset != null)
        {
            ProductArray datosPrefab = JsonUtility.FromJson<ProductArray>(jsonAsset.text);
            products = new GameObject[datosPrefab.products.Length];

            int i = 0;
            foreach (Product product in datosPrefab.products)
            {
                GameObject newPrefab = Instantiate(basePrefab, transform);

                // Set Sprite
                SpriteRenderer sprite = newPrefab.GetComponent<SpriteRenderer>();
                Texture2D tex = (Texture2D)Resources.Load(product.spritePath);
                sprite.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                sprite.color = new Color(product.color.r / 255f, 
                                    product.color.g / 255f, 
                                    product.color.b / 255f, 
                                    product.color.a / 255f);
                
                // Set product info
                ProductInfo productInfo = newPrefab.GetComponent<ProductInfo>();
                productInfo.Product = product;

                newPrefab.SetActive(false);
                products[i] = newPrefab;

                i++;
            }
        }
    }
}
