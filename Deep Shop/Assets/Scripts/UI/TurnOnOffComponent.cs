using UnityEngine;

public class TurnOnOffComponent : MonoBehaviour
{
    public MonoBehaviour _component;
    public void SwitchComponent()
    {
        _component.enabled = !_component.enabled;
    }
}
