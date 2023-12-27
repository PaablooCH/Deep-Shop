using UnityEngine;
using TMPro;

public class DisableButtonNumbCondition : DisableButton
{
    [SerializeField]
    private TMP_InputField _inputField;
    
    [SerializeField]
    private int _lessThan = 0;
    [SerializeField]
    private bool _lessResult = true;
    [SerializeField]
    private int _equal = 0;
    [SerializeField]
    private bool _equalResult = true;
    [SerializeField]
    private int _greaterThan = 0;
    [SerializeField]
    private bool _greaterResult = true;

    // Update is called once per frame
    void Update()
    {
        if (_inputField && _button)
        {
            bool previousState = _button.interactable;

            if (float.TryParse(_inputField.text, out float inputValue))
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
