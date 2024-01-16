using UnityEngine;

public class PauseManager : MonoBehaviour
{
    #region Singleton
    public static PauseManager instance;

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

    private bool _paused = false;
    public void Pause()
    {
        if (!_paused)
        {
            Time.timeScale = 0f;
            _paused = true;
        }
    }

    public void Restart()
    {
        if (_paused)
        {
            Time.timeScale = 1f;
            _paused = false;
        }
    }
}
