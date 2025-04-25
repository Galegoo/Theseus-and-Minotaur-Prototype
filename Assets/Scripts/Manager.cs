using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    GameObject player;
    GameObject minotaur;

    int undoCount;
    [SerializeField]
    TMP_Text undoCountText;
    [SerializeField]
    TMP_Text playsCountText;
    [SerializeField]
    TMP_Text exit;

    public bool gameover;
    [SerializeField]
    private GameObject gameOverPopUp;
    [SerializeField]
    private GameObject WinPopUp;

    public GameObject path;
    public GameObject W;


    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Player");
        minotaur = GameObject.Find("Minotaur");
        gameover = false;
        path.SetActive(false);
        W.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Calls Restart method when Space Bar is pressed 
            {
                RestartLevel();
            }

            if (Input.GetKeyDown(KeyCode.W)) // Calls Wait method when W is pressed
            {
                Wait();
            }

            if (Input.GetKeyDown(KeyCode.Z)) // Calls Undo method when Z is pressed
            {
                Undo();
            }

            if (Input.GetKeyDown(KeyCode.P)) // Calls Undo method when Z is pressed
            {
                Path();
            }
        }
        undoCountText.text = "Undos: " + undoCount;
        playsCountText.text = "Plays: " + player.GetComponent<PlayerMovment>().plays;
    }
    public void RestartLevel()// RESTART LEVEL
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void Wait()// makes a play without moving
    {
        player.GetComponent<PlayerMovment>().plays++;
    }

    private void Undo() // sets the position of minotaur and player to their last play
    {
        player.transform.position = player.GetComponent<PlayerMovment>().playerMovementStorage;
        minotaur.transform.position = minotaur.GetComponent<MinotaurMovment>().minotaurMovementStorage;
        player.GetComponent<PlayerMovment>().plays--;
        minotaur.GetComponent<MinotaurMovment>().playsstorage--;
        undoCount++;
    }

    public void GameOver()//Method that is called when the minotaur gets to the player before the player gets to the exit
    {
        if(gameover == false)
        {
            gameover = true; //bool that doenst allow the controls to be used
            gameOverPopUp.SetActive(true); //sets the game over pop up 
            exit.gameObject.SetActive(false);
        }
    }

    public void Win() //Method that is called when the player gets to the exit sucessfully
    {
        gameover = true; //bool that doenst allow the controls to be used
        WinPopUp.SetActive(true); //sets the game over pop up 
        exit.gameObject.SetActive(false);
    }

    public void GoBackToTheMainMenu() //go back to the menu Method
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToTheNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        
        else if (scene.name == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void Path()
    {
        if(path.activeSelf == true)
        {
            path.SetActive(false);
            W.SetActive(false);
        }
        else
        {
            path.SetActive(true);
            W.SetActive(true);
        }
    }
}
