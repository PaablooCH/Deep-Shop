using UnityEngine;

public class MenuWindowUI : MonoBehaviour, IUI
{
    [SerializeField]
    private GameObject _predeterminedTab;

    private GameObject _activeTab;

    // Pre Open Tabs conditions
    private bool _gridCreated = false;

    private void Start()
    {
        _activeTab = _predeterminedTab;
    }

    public void ChangeTab(GameObject tab)
    {
        CheckPreOpenTab(tab);
        if (tab != _activeTab)
        {
            _activeTab.SetActive(false);
            _activeTab = tab;
            _activeTab.SetActive(true);
        }
    }

    private void CheckPreOpenTab(GameObject tab)
    {
        if (!_gridCreated && tab.TryGetComponent(out CraftUI craftUI))
        {
            craftUI.CreateGrid();
            _gridCreated = true;
        }
    }

    public void OpenUI(GameObject go)
    {
        _activeTab = _predeterminedTab;
        _activeTab.SetActive(true);
    }

    public void Exit()
    {
        _activeTab.SetActive(false);
    }
}
