using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    #region Listeners
    public delegate void OnKarmaChanged(float newKarma, float oldKarma);
    public OnKarmaChanged onKarmaChanged;
    #endregion

    [SerializeField]
    private float money = 100f;
    [SerializeField]
    [Range(-100f, 100f)]
    private float karma = 0f;

    public float Karma { get => karma; 
        set 
        {
            if (onKarmaChanged != null)
            {
                onKarmaChanged(value, karma);
            }
            karma = value; 
        }
    }

    public void Trade(ProductInfo product, int n, float price)
    {
        InventoryManager.instance.ModifyInventory(product.Product.productType, -n);
        Karma += product.CalculateKarma(price);
        if (product.CalculatePercentatgeBuy(price) < 2.5f)
        {
            money += price;
        }
    }
}
