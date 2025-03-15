using UnityEngine;
using UnityEngine.UI;
public class PauseMenuUI : MonoBehaviour
{ 
    public GameObject gamePauseMenuUI;
    public bool isGamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGamePaused = false;
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
}
