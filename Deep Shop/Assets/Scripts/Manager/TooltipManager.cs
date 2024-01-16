using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region Singleton
    public static TooltipManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField]
    TooltipUI _tooltipUI;

    // TODO cuando esta activo y se cambia de ventana se queda activo
    public void Show(string body, string header = "")
    {
        _tooltipUI.CleanGameObjectsAdded();
        _tooltipUI.SetText(body, header);
        _tooltipUI.gameObject.SetActive(true);
        _tooltipUI.MoveTooltipToMouse();
    }

    public void Hide()
    {
        _tooltipUI.gameObject.SetActive(false);
    }

    public void AddGameObjects(GameObject[] gos)
    {
        _tooltipUI.AddGameObjects(gos);
    }

    public void CleanGameObjectsAdded()
    {
        _tooltipUI.CleanGameObjectsAdded();
    }
}
