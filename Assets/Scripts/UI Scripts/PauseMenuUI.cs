using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
public class PauseMenuUI : MonoBehaviour
{ 
    public GameObject gamePauseMenuUI;
    public bool isGamePaused = false;
    public Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGamePaused = false;
        Button gameQuits = quitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(gameQuit);
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
    
}
