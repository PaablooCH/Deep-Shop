using System.Collections;
using System.Collections.Generic;
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

    public void Show(string body, string header = "")
    {
        _tooltipUI.SetText(body, header);
        _tooltipUI.gameObject.SetActive(true);
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
