using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float money = 100f;
    [SerializeField]
    [Range(-100f, 100f)]
    private float karma = 0f;

    private Dictionary<ProductType, int> inventory;

    public float Karma { get => karma; set => karma = value; }
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

    public void Trade(Product product, int n, float price)
    {
        ModifyInventory(product.ProductType, -n);
        karma += product.CalculateKarma(price);
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
}
