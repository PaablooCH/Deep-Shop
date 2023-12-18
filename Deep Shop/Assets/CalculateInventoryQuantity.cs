using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateInventoryQuantity : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private ProductType product;
    [SerializeField]
    private TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = playerStats.GetInventory(product).ToString();
    }
}
