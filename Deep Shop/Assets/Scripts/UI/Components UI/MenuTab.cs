using UnityEngine;

public class MenuTab : MonoBehaviour
{
    [SerializeField] private MenuWindowUI _menuWindow;
    [SerializeField] private GameObject _menuTabWindow;

    private GameObject _notSelected;
    private GameObject _selected;

    public GameObject MenuTabWindow { get => _menuTabWindow; set => _menuTabWindow = value; }

    private void Awake()
    {
        _notSelected = transform.Find("Not Selected").gameObject;
        _selected = transform.Find("Selected").gameObject;
    }

    public void SelectTab()
    {
        _menuWindow.ChangeTab(this);
        ActiveGameObjects(true);
    }

    public void DeselectTab()
    {
        ActiveGameObjects(false);
    }

    public void ActiveGameObjects(bool configuration)
    {
        _notSelected.SetActive(!configuration);
        _selected.SetActive(configuration);

        _menuTabWindow.SetActive(configuration);
    }
}
