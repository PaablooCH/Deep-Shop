using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    INVENTORY_KARMA,
    TRADE,
    SHOP
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
        bool activeBackUp;

        public MyUI(GameObject go)
        {
            uiGameObject = go;
            activeBackUp = false;
        }

        public GameObject UiGameObject { get => uiGameObject; set => uiGameObject = value; }
        public bool ActiveBackUp { get => activeBackUp; set => activeBackUp = value; }
    }

    [SerializeField]
    private GameObject inventoryAndKarma;

    private Dictionary<UIType, MyUI> uis = new();

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
        uis[ui] = new MyUI(gameObjectUI);
    }

    public void ActiveTradeUI()
    {
        PauseManager.instance.Pause();
        uis[UIType.TRADE].UiGameObject.SetActive(true);
    }

    private void ActiveInventory()
    {
        CreateBackUp();
        uis[UIType.INVENTORY_KARMA].UiGameObject.SetActive(true);
    }

    public void ActiveShop()
    {
        // TODO
    }

    public void FreeUI(UIType ui)
    {
        PauseManager.instance.Restart();
        uis[ui].UiGameObject.SetActive(false);
        uis[ui].ActiveBackUp = false;
        RestoreBackUp();
    }

    private void CreateBackUp()
    {
        if (backUp)
        {
            return;
        }

        foreach (MyUI myUI in uis.Values)
        {
            myUI.ActiveBackUp = myUI.UiGameObject.activeInHierarchy;
            myUI.UiGameObject.SetActive(false);
        }
        backUp = true;
    }

    private void RestoreBackUp()
    {
        foreach(MyUI myUI in uis.Values)
        {
            myUI.UiGameObject.SetActive(myUI.ActiveBackUp);
            myUI.ActiveBackUp = false;
        }
        backUp = false;
    }

    private void ClearBackUp()
    {
        foreach (MyUI myUI in uis.Values)
        {
            myUI.ActiveBackUp = false;
        }
    }
}
