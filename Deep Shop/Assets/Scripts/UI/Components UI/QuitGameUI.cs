using UnityEditor;
using UnityEngine;

public class QuitGameUI : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }
}
