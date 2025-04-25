using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Play() //start the game
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Quit() //quit the game
    {
        Application.Quit();
    }
}
