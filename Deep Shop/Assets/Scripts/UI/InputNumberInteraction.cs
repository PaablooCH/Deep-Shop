using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNumberInteraction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputText;

    private int _amountProduct = 0;

    public int AmountProduct { get => _amountProduct; set => _amountProduct = value; }

    public void AddValue()
    {
        int numericValue = int.Parse(_inputText.text);
        if (numericValue < _amountProduct)
        {
            numericValue++;
        }
        _inputText.text = numericValue.ToString();
    }

    public void SubstractValue()
    {
        int numericValue = int.Parse(_inputText.text);
        if (numericValue > 0)
        {
            numericValue--;
        }
        _inputText.text = numericValue.ToString();
    }
}
