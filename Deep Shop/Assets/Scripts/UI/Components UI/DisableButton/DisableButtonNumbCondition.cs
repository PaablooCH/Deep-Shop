using UnityEngine;
using TMPro;

public class DisableButtonNumbCondition : DisableButton
{  
    [SerializeField] private int _lessThan = 0;
    [SerializeField] private bool _lessResult = true;
    
    [SerializeField] private int _equal = 0;
    [SerializeField] private bool _equalResult = true;
    
    [SerializeField] private int _greaterThan = 0;
    [SerializeField] private bool _greaterResult = true;

    public void DisableByInputField(TMP_InputField inputField)
    {
        if (inputField && _button)
        {
            bool previousState = _button.interactable;

            if (float.TryParse(inputField.text, out float inputValue))
            {
                bool equalCondition = _equalResult && inputValue == _equal;
                bool greaterCondition = _greaterResult && inputValue > _greaterThan;
                bool lessCondition = _lessResult && inputValue < _lessThan;

                // The field is changed based on the results
                _button.interactable = equalCondition || greaterCondition || lessCondition;
            }
            else
            {
                // Turn to false if the parse fails
                _button.interactable = false;
            }

            // Some possible conditions
            if (_button.interactable != previousState)
            {
            }
        }
    }
}
