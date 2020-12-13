using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseUI;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = togglePause();
            Debug.Log("PAUSE PRESSED");
            //Cursor.visible = true;
        }
    }

    void OnGUI()
    {
        if (paused)
        {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Click me to unpause"))
                paused = togglePause();
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);

        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}
