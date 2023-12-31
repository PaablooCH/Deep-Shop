using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    INVENTORY,
    TRADE,
    PANEL_SHOP
}

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

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

    private class MyUI
    {
        GameObject uiGameObject;
        UIType type;
        bool activeBackUp;

        public MyUI(GameObject go, UIType type)
        {
            uiGameObject = go;
            this.type = type;
            activeBackUp = false;
        }

        public GameObject UiGameObject { get => uiGameObject; set => uiGameObject = value; }
        public bool ActiveBackUp { get => activeBackUp; set => activeBackUp = value; }
        public UIType Type { get => type; set => type = value; }
    }

    private List<MyUI> _uis = new(); // small number of UI and I can search iteratively

    private bool _backUp = false;

    public void AddUI(UIType ui, GameObject gameObjectUI)
    {
        _uis.Add(new MyUI(gameObjectUI, ui));
    }

    public void ActiveTradeUI()
    {
        PauseManager.instance.Pause();
        SearchByType(UIType.TRADE).UiGameObject.SetActive(true);
    }

    public void ActiveInventory()
    {
        CreateBackUp();
        SearchByType(UIType.INVENTORY).UiGameObject.SetActive(true);
    }

    public void ActivePanelShop()
    {
        PauseManager.instance.Pause();
        SearchByType(UIType.PANEL_SHOP).UiGameObject.SetActive(true);
    }

    public void FreeUI(UIType type)
    {
        PauseManager.instance.Restart();
        MyUI ui = SearchByType(type);
        ui.UiGameObject.SetActive(false);
        ui.ActiveBackUp = false;
        RestoreBackUp();
    }

    private MyUI SearchByType(UIType type)
    {
        foreach(MyUI ui in _uis)
        {
            if (ui.Type == type)
            {
                return ui;
            }
        }
        return null;
    }

    private void CreateBackUp()
    {
        if (_backUp)
        {
            return;
        }

        foreach (MyUI myUI in _uis)
        {
            myUI.ActiveBackUp = myUI.UiGameObject.activeInHierarchy;
            myUI.UiGameObject.SetActive(false);
        }
        _backUp = true;
    }

    private void RestoreBackUp()
    {
        foreach(MyUI myUI in _uis)
        {
            myUI.UiGameObject.SetActive(myUI.ActiveBackUp);
            myUI.ActiveBackUp = false;
        }
        _backUp = false;
    }

    private void ClearBackUp()
    {
        foreach (MyUI myUI in _uis)
        {
            myUI.ActiveBackUp = false;
        }
    }
}
