using UnityEngine;

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

    [SerializeField]
    private GameObject _menuUI;

    private GameObject _actualUI;

    private bool _opened = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_opened)
            {
                FreeUI();
                CallExit();
                if (_actualUI.TryGetComponent(out MenuWindowUI menu))
                {
                    menu.Exit();
                }
            }
            else
            {
                ActiveUI(_menuUI);
                MenuWindowUI menu = _actualUI.GetComponent<MenuWindowUI>();
                menu.OpenUI(null);
            }
        }
    }

    private void CallExit()
    {
        if (_actualUI.TryGetComponent(out MenuWindowUI menu))
        {
            menu.Exit();
        }
        else if (_actualUI.TryGetComponent(out ShopUI shop))
        {
            shop.Exit();
        }
        else if (_actualUI.TryGetComponent(out ItemsAcquiredUI itemAcquired))
        {
            itemAcquired.Exit();
        }
        else if (_actualUI.TryGetComponent(out TradeUI trade))
        {
            trade.Exit();
        }
    }

    public void ActiveUI(GameObject newUI)
    {
        PauseManager.instance.Pause();
        _actualUI = newUI;
        _actualUI.SetActive(true);
        _opened = true;
    }

    public void FreeUI()
    {
        PauseManager.instance.Restart();
        _actualUI.SetActive(false);
        _opened = false;
        TooltipManager.instance.Hide();
    }
}
