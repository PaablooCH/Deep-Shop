using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //public GameObject menuPause;
    //private bool paused = false;

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
        Time.timeScale = 0f; // Pause
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Resume
    }
}
