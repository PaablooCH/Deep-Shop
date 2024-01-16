using UnityEngine;

public class MenuTab : MonoBehaviour
{
    [SerializeField]
    private MenuWindowUI _menuWindow;
    [SerializeField]
    private GameObject _menuTab;
    
    public void SelectTab()
    {
        _menuWindow.ChangeTab(_menuTab);
    }
}
