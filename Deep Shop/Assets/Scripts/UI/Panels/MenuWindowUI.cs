using UnityEngine;

public class MenuWindowUI : MonoBehaviour, IUI
{
    [SerializeField]
    private MenuTab _predeterminedTab;

    private MenuTab _activeMenuTab;

    // Pre Open Tabs conditions
    private bool _gridCreated = false;

    public void ChangeTab(MenuTab menuTab)
    {
        CheckPreOpenTab(menuTab.MenuTabWindow);
        if (menuTab != _activeMenuTab)
        {
            _activeMenuTab.DeselectTab();
            _activeMenuTab = menuTab;
        }
    }

    private void CheckPreOpenTab(GameObject tabWindow)
    {
        if (!_gridCreated && tabWindow.TryGetComponent(out CraftUI craftUI))
        {
            craftUI.CreateGrid();
            _gridCreated = true;
        }
    }

    public void OpenUI()
    {
        _predeterminedTab.ActiveGameObjects(true);
        _activeMenuTab = _predeterminedTab;
    }

    public void Exit()
    {
        _activeMenuTab.DeselectTab();
    }
}
