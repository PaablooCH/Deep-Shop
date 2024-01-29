using UnityEngine;

public enum UIs
{
    MENU, 
    SHOP, 
    TRADE,
    ITEM_ACQ,
    DIALOGUE
}

public class CanvasManager : MonoBehaviour
{
    #region Singleton
    public static CanvasManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Canvas Manager singleton already exists.");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _tradeUI;
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private GameObject _itemAcqUI;
    [SerializeField] private GameObject _dialogueUI;

    private GameObject _actualPanel;

    private bool _opened = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_opened)
            {
                FreeUI();
                CallExit();
                if (_actualPanel.TryGetComponent(out MenuWindowUI menu))
                {
                    menu.Exit();
                }
            }
            else
            {
                ActiveUI(UIs.MENU);
                MenuWindowUI menu = _actualPanel.GetComponent<MenuWindowUI>();
                menu.OpenUI();
            }
        }
    }

    private void CallExit()
    {
        if (_actualPanel.TryGetComponent(out MenuWindowUI menu))
        {
            menu.Exit();
        }
        else if (_actualPanel.TryGetComponent(out ShopUI shop))
        {
            shop.Exit();
        }
        else if (_actualPanel.TryGetComponent(out ItemsAcquiredUI itemAcquired))
        {
            itemAcquired.Exit();
        }
        else if (_actualPanel.TryGetComponent(out TradeUI trade))
        {
            trade.Exit();
        }
        else if (_actualPanel.TryGetComponent(out DialogueUI dialogue))
        {
            dialogue.EndConversation();
        }
    }

    public void ActiveUI(UIs ui)
    {
        PauseManager.instance.Pause();
        _actualPanel = GetPanel(ui);
        
        _actualPanel.SetActive(true);
        _opened = true;
    }

    public GameObject GetPanel(UIs ui)
    {
        switch (ui)
        {
            case UIs.MENU:
                return _menuUI;
            case UIs.SHOP:
                return _shopUI;
            case UIs.TRADE:
                return _tradeUI;
            case UIs.ITEM_ACQ:
                return _itemAcqUI;
            case UIs.DIALOGUE:
                return _dialogueUI;
            default:
                Debug.LogWarning("Panel: " + ui + " doesn't exist.");
                return null;
        }
    }

    public void FreeUI()
    {
        PauseManager.instance.Restart();
        _actualPanel.SetActive(false);
        _opened = false;
        TooltipManager.instance.Hide();
    }
}
