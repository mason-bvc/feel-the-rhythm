using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    
    public Button mainMenuButton; 
    public Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //For Main Menu Button:
        Button mainMenu = mainMenuButton.GetComponent<Button>();
        mainMenu.onClick.AddListener(startGame);
        
        //For quit button:
        Button quitButton = this.quitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(startGame);
    }

    public void startGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
