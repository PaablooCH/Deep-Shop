using System.Collections;
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
        StartCoroutine(LoadProductsAsync());
    }
    #endregion

    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private GameObject[] _products;

    private int _totalWeightSpawn = 0;

    private const string PRODUCTS_PATH = "JSONs/products";

    public GameObject[] Products { get => _products; set => _products = value; }
    public int TotalWeightSpawn { get => _totalWeightSpawn; set => _totalWeightSpawn = value; }

    public GameObject SearchProductByID(int id)
    {
        foreach(GameObject go in _products)
        {
            if (go.GetComponent<ProductInfo>().Product.id == id)
            {
                return go;
            }
        }
        return null;
    }

    public ProductInfo GetProductInfo(int id)
    {
        GameObject obj = SearchProductByID(id);
        if (obj != null)
        {
            return obj.GetComponent<ProductInfo>();
        }
        return null;
    }

    public GameObject SearchProductByName(string name)
    {
        foreach (GameObject go in _products)
        {
            if (go.GetComponent<ProductInfo>().Product.productName == name)
            {
                return go;
            }
        }
        return null;
    }

    public int RandomProductID()
    {
        return RandomProduct().GetComponent<ProductInfo>().Product.id;
    }

    public GameObject RandomProduct()
    {
        int randomWeight = Random.Range(1, _totalWeightSpawn + 1);

        int actualWeight = 0;
        foreach (GameObject product in _products)
        {
            int productWeight = product.GetComponent<ProductInfo>().Product.weightSpawn;
            actualWeight += productWeight;
            if (randomWeight <= actualWeight)
            {
                return product;
            }
        }

        // If we don't find anything, we return the last product.
        return _products[_products.Length - 1];
    }

    private IEnumerator LoadProductsAsync()
    {
        ResourceRequest request = Resources.LoadAsync<TextAsset>(PRODUCTS_PATH);

        while (!request.isDone)
        {
            float progress = request.progress;
            Debug.Log("Products load progress: " + progress * 100f + "%");
            yield return null;
        }
        Debug.Log("Products load progress: 100%");

        TextAsset jsonAsset = request.asset as TextAsset;

        if (jsonAsset != null)
        {
            ProductArray dataPrefab = JsonUtility.FromJson<ProductArray>(jsonAsset.text);
            _products = new GameObject[dataPrefab.products.Length];

            int i = 0;
            foreach (Product product in dataPrefab.products)
            {
                GameObject newPrefab = Instantiate(_basePrefab, transform);

                // Set Sprite
                SpriteRenderer spriteRender = newPrefab.GetComponent<SpriteRenderer>();
                spriteRender.sprite = UtilsLoadResource.LoadSprite(product.spritePath);
                spriteRender.color = new Color(product.color.r / 255f,
                                    product.color.g / 255f,
                                    product.color.b / 255f,
                                    product.color.a / 255f);

                // Set product info
                ProductInfo productInfo = newPrefab.GetComponent<ProductInfo>();
                productInfo.Product = product;

                _totalWeightSpawn += product.weightSpawn;

                newPrefab.SetActive(false);
                _products[i] = newPrefab;

                i++;
            }
        }
        WaitInventory.instance.ProductsReady = true;
    }
}
