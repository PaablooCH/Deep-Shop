using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipGameObjectTrigger : TooltipTrigger
{
    private GameObject[] _gameObjectsTooltip;

    public GameObject[] GameObjectsTooltip { get => _gameObjectsTooltip; set => _gameObjectsTooltip = value; }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        TooltipManager.instance.AddGameObjects(_gameObjectsTooltip);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        TooltipManager.instance.CleanGameObjectsAdded();
    }
}
