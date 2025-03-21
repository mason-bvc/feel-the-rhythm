using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{ 
    public GameObject gamePauseMenuUI;
    public bool isGamePaused = false;
    public Button quitButton;
    public Button mainMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGamePaused = false;
        //Button Section:
        //Game Quit button:
        Button gameQuits = quitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(gameQuit);
        
        //Main Menu button:
        Button mainMenuClick = mainMenuButton.GetComponent<Button>();
        mainMenuClick.onClick.AddListener(mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();   
            }
            else
            {
                GamePause();
            }
           
        }
        
    }
    public void GamePause()
    {
        Time.timeScale = 0;
        gamePauseMenuUI.SetActive (true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePauseMenuUI.SetActive (false);
        isGamePaused = false;
    }

    public void gameQuit()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }
    
}
