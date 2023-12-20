using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Slider evilSlider;
    [SerializeField]
    private Slider goodSlider;

    [SerializeField]
    private float money = 100f;
    [SerializeField]
    [Range(-100f, 100f)]
    private float karma = 0f;

    private Dictionary<ProductType, int> inventory;

    public float Karma { get => karma; 
        set 
        {
            UpdateKarmaSliders(value);            
            karma = value; 
        } 
    }

    public Dictionary<ProductType, int> Inventory { get => inventory; set => inventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<ProductType, int>();
        inventory[ProductType.LEGAL_1] = 5;
        inventory[ProductType.LEGAL_2] = 5;
        inventory[ProductType.LEGAL_3] = 2;
        inventory[ProductType.NOT_LEGAL_1] = 3;
        inventory[ProductType.NOT_LEGAL_2] = 1;
        inventory[ProductType.NOT_LEGAL_3] = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetInventory(ProductType productType)
    {
        return inventory[productType];
    }

    public void Trade(ProductInfo product, int n, float price)
    {
        ModifyInventory(product.Product.productType, -n);
        Karma += product.CalculateKarma(price);
        if (product.CalculatePercentatgeBuy(price) < 2.5f)
        {
            money += price;
        }
    }

    // n can be negative (substract) or positive (sum)
    private void ModifyInventory(ProductType type, int n)
    {
        if (inventory[type] + n < 0)
        {
            Debug.LogError("Attempt to leave a negative value in the inventory");
            return;
        }
        inventory[type] += n;
    }

    private void UpdateKarmaSliders(float value)
    {
        if (evilSlider != null && goodSlider != null)
        {
            if (value == 0)
            {
                evilSlider.value = 0;
                goodSlider.value = 0;
            }
            if (value > 0)
            {
                if (karma < 0)
                {
                    evilSlider.value = 0; // Reset old predominant Slider
                }
                goodSlider.value = value;
            }
            else
            {
                if (karma > 0)
                {
                    goodSlider.value = 0; // Reset old predominant Slider
                }
                evilSlider.value = -value;
            }
        }
    }
}
