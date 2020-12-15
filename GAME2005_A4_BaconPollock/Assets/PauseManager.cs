using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseUI;
    public GameObject playerController;
    
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = togglePause();
            Debug.Log("PAUSE PRESSED");
            // Cursor.visible = true;
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
            //controller.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerController.GetComponent<PlayerBehaviour>().enabled = false;

        }
        else
        {
            pauseUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerController.GetComponent<PlayerBehaviour>().enabled = true;

            // playerController.GetComponent<CharacterController>().enabled = true;
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
