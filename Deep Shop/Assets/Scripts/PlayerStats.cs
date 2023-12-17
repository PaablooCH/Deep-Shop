using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float money = 100f;

    [SerializeField]
    [Tooltip("Legal Product 1 inventory")]
    [Min(0)]
    private int lp1Inventory = 5;
    [SerializeField]
    [Tooltip("Legal Product 2 inventory")]
    [Min(0)]
    private int lp2Inventory = 5;
    [SerializeField]
    [Tooltip("Legal Product 3 inventory")]
    [Min(0)]
    private int lp3Inventory = 2;
    [SerializeField]
    [Tooltip("Not Legal Product 1 inventory")]
    [Min(0)]
    private int nlp1Inventory = 3;
    [SerializeField]
    [Tooltip("Not Legal Product 2 inventory")]
    [Min(0)]
    private int nlp2Inventory = 1;
    [SerializeField]
    [Tooltip("Not Legal Product 3 inventory")]
    [Min(0)]
    private int nlp3Inventory = 1;

    private float karma = 0f;

    public float Karma { get => karma; set => karma = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetInventory(Product product)
    {
        switch (product.ProductType)
        {
            case ProductType.LEGAL_1:
                return lp1Inventory;
            case ProductType.LEGAL_2:
                return lp2Inventory;
            case ProductType.LEGAL_3:
                return lp3Inventory;
            case ProductType.NOT_LEGAL_1:
                return nlp1Inventory;
            case ProductType.NOT_LEGAL_2:
                return nlp2Inventory;
            case ProductType.NOT_LEGAL_3:
                return nlp3Inventory;
        }
        return -1;
    }
}
