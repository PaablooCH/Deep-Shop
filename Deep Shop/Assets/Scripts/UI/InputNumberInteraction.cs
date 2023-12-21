using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNumberInteraction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputText;

    private int amountProduct = 0;

    public int AmountProduct { get => amountProduct; set => amountProduct = value; }

    public void AddValue()
    {
        int numericValue = int.Parse(inputText.text);
        if (numericValue < amountProduct)
        {
            numericValue++;
        }
        inputText.text = numericValue.ToString();
    }

    public void SubstractValue()
    {
        int numericValue = int.Parse(inputText.text);
        if (numericValue > 0)
        {
            numericValue--;
        }
        inputText.text = numericValue.ToString();
    }
}
