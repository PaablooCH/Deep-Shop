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

    public delegate void OnMoneyChanged(float newMoney);
    public OnMoneyChanged onMoneyChanged;
    #endregion

    [SerializeField]
    [Range(0f, float.MaxValue)]
    private float _money = 100f;
    [SerializeField]
    [Range(-100f, 100f)]
    private float _karma = 0f;

    public float Karma { get => _karma; 
        set 
        {
            onKarmaChanged?.Invoke(value, _karma);
            _karma = value; 
        }
    }

    public float Money { get => _money;
        set
        {
            onMoneyChanged?.Invoke(value);
            _money = value;
        }
    }

    public void Trade(ProductInfo product, int n, float price)
    {
        InventoryManager.instance.ModifyInventory(product.Product.id, -n);
        Karma += product.CalculateKarma(price);
        if (product.CalculatePercentatgeBuy(price) < 2.5f)
        {
            Money += price;
        }
    }
}
