using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start Button:
        Button gameStarts = startButton.GetComponent<Button>();
        startButton.onClick.AddListener(gameStart);
        //Settings Button:
        
        
        //Quit Button:
        Button gameQuits = quitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(quitGame);
    }

    public void gameStart()
    {
        SceneManager.LoadScene(2);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    
}
