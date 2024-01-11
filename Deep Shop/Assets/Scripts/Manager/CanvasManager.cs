using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    INVENTORY,
    TRADE,
    SHOP,
    ITEMS_ACQUIRED,
    CRAFT
}

public class CanvasManager : MonoBehaviour
{
    #region Singleton
    public static CanvasManager instance;

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
        bool gamePaused;

        public MyUI(GameObject go, UIType type)
        {
            uiGameObject = go;
            this.type = type;
            activeBackUp = false;
            gamePaused = false;
        }

        public GameObject UiGameObject { get => uiGameObject; set => uiGameObject = value; }
        public bool ActiveBackUp { get => activeBackUp; set => activeBackUp = value; }
        public UIType Type { get => type; set => type = value; }
        public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    }

    private List<MyUI> _uis = new(); // small number of UI and I can search iteratively

    private bool _backUp = false;

    public void AddUI(UIType ui, GameObject gameObjectUI)
    {
        _uis.Add(new MyUI(gameObjectUI, ui));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActiveUI(UIType.CRAFT);
        }
    }

    public void ActiveUI(UIType type)
    {
        if (type == UIType.INVENTORY)
        {
            CreateBackUp();
        }
        PauseManager.instance.Pause();
        MyUI myUI = SearchByType(type);
        myUI.UiGameObject.SetActive(true);
        myUI.GamePaused = true;
    }

    public void FreeUI(UIType type)
    {
        PauseManager.instance.Restart();
        MyUI ui = SearchByType(type);
        ui.UiGameObject.SetActive(false);
        ui.ActiveBackUp = false;
        ui.GamePaused = false;
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
            if (myUI.GamePaused)
            {
                PauseManager.instance.Pause();
            }
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
