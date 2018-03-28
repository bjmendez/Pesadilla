using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //called if the start button is pressed, loads the game
    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
