using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    [SerializeField]
    protected Button _button;

    public void TurnOff()
    {
        if (_button)
        {
            if (_button.interactable)
            {
                _button.interactable = false;
            }
        }
    }

    public void TurnOn()
    {
        if (_button)
        {
            if (!_button.interactable)
            {
                _button.interactable = true;
            }
        }
    }
}
