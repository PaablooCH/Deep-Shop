using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    INVENTORY_KARMA,
    TRADE,
    PANEL_SHOP,
    BUTTON_SHOP
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

    [SerializeField]
    private GameObject inventoryAndKarma;

    private List<MyUI> uis = new(); // small number of UI and I can search iteratively

    private bool backUp = false;

    private void Start()
    {
        if (inventoryAndKarma != null)
        {
            inventoryAndKarma.SetActive(false);
            AddUI(UIType.INVENTORY_KARMA, inventoryAndKarma);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            ActiveInventory();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            FreeUI(UIType.INVENTORY_KARMA);
        }
    }

    public void AddUI(UIType ui, GameObject gameObjectUI)
    {
        uis.Add(new MyUI(gameObjectUI, ui));
    }

    public void ActiveTradeUI()
    {
        PauseManager.instance.Pause();
        CreateBackUp();
        SearchByType(UIType.TRADE).UiGameObject.SetActive(true);
    }

    private void ActiveInventory()
    {
        CreateBackUp();
        SearchByType(UIType.INVENTORY_KARMA).UiGameObject.SetActive(true);
    }

    public void ActivePanelShop()
    {
        PauseManager.instance.Pause();
        CreateBackUp();
        SearchByType(UIType.PANEL_SHOP).UiGameObject.SetActive(true);
        SearchByType(UIType.BUTTON_SHOP).UiGameObject.SetActive(false);
    }

    public void ActiveButtonShop()
    {
        SearchByType(UIType.BUTTON_SHOP).UiGameObject.SetActive(true);
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
        foreach(MyUI ui in uis)
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
        if (backUp)
        {
            return;
        }

        foreach (MyUI myUI in uis)
        {
            myUI.ActiveBackUp = myUI.UiGameObject.activeInHierarchy;
            myUI.UiGameObject.SetActive(false);
        }
        backUp = true;
    }

    private void RestoreBackUp()
    {
        foreach(MyUI myUI in uis)
        {
            myUI.UiGameObject.SetActive(myUI.ActiveBackUp);
            myUI.ActiveBackUp = false;
        }
        backUp = false;
    }

    private void ClearBackUp()
    {
        foreach (MyUI myUI in uis)
        {
            myUI.ActiveBackUp = false;
        }
    }
}
