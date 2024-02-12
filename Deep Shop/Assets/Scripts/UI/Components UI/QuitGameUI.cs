#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class QuitGameUI : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
