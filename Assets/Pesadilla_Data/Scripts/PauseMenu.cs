using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenu;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }

    }

    //Pauses all animated objects, called when esc is pressed in game
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

    }
    //Resumes all animation of all obejcts, called when ResumeButton is pressed
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

    }
}
