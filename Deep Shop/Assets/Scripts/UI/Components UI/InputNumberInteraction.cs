using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNumberInteraction : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    private int _upperLimit = int.MaxValue;
    private int _lowerLimit = int.MinValue;

    public int UpperLimit { get => _upperLimit; set => _upperLimit = value; }
    public int LowerLimit { get => _lowerLimit; set => _lowerLimit = value; }

    public void AddValue()
    {
        int numericValue = int.Parse(_inputField.text);
        if (numericValue < _upperLimit)
        {
            numericValue++;
        }
        _inputField.text = numericValue.ToString();
    }

    public void SubstractValue()
    {
        int numericValue = int.Parse(_inputField.text);
        if (numericValue > _lowerLimit)
        {
            numericValue--;
        }
        _inputField.text = numericValue.ToString();
    }
}
