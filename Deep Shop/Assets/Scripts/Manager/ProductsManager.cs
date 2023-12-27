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
    #region Singleton
    public static ProductsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
        ReadJSON();
    }
    #endregion

    [SerializeField]
    private GameObject _basePrefab;
    [SerializeField]
    private GameObject[] _products;
    private int _totalWeightSpawn = 0;

    public GameObject[] Products { get => _products; set => _products = value; }
    public int TotalWeightSpawn { get => _totalWeightSpawn; set => _totalWeightSpawn = value; }

    public GameObject SearchProductByID(int id)
    {
        foreach(GameObject go in _products)
        {
            if (go.GetComponent<ProductInfo>().product.id == id)
            {
                return go;
            }
        }
        return null;
    }

    public GameObject SearchProductByName(string name)
    {
        foreach (GameObject go in _products)
        {
            if (go.GetComponent<ProductInfo>().product.name == name)
            {
                return go;
            }
        }
        return null;
    }

    public GameObject RandomProduct()
    {
        int randomWeight = Random.Range(1, _totalWeightSpawn + 1);

        int actualWeight = 0;
        foreach (GameObject product in _products)
        {
            int productWeight = product.GetComponent<ProductInfo>().product.weightSpawn;
            actualWeight += productWeight;
            if (randomWeight <= actualWeight)
            {
                return product;
            }
        }

        // If we don't find anything, we return the last product.
        return _products[_products.Length - 1];
    }

    private void ReadJSON()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("products");
        if (jsonAsset != null)
        {
            ProductArray dataPrefab = JsonUtility.FromJson<ProductArray>(jsonAsset.text);
            _products = new GameObject[dataPrefab.products.Length];

            int i = 0;
            foreach (Product product in dataPrefab.products)
            {
                GameObject newPrefab = Instantiate(_basePrefab, transform);

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
                productInfo.product = product;

                _totalWeightSpawn += product.weightSpawn;

                newPrefab.SetActive(false);
                _products[i] = newPrefab;

                i++;
            }
        }
    }
}
