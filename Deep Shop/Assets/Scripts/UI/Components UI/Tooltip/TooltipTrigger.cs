using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected string _header;
    [SerializeField]
    protected string _body;

    public string Header { get => _header; set => _header = value; }
    public string Body { get => _body; set => _body = value; }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.Show(_body, _header);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.Hide();
    }
}
