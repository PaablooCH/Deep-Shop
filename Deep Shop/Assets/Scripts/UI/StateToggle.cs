using UnityEngine;
using UnityEngine.UI;

public class StateToggle : MonoBehaviour
{
    public void SwitchToggle(bool state)
    {
        GetComponent<Toggle>().isOn = state;
    }
}
