using System.Collections;
using System.Collections.Generic;
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

    //public GameObject menuPause;
    private bool _paused = false;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (paused)
        //    {
        //        Restart();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
    }

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
