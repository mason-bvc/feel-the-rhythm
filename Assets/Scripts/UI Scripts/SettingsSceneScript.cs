using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneScript : MonoBehaviour
{
    public Button backButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Back Button:
        Button backClick = backButton.GetComponent<Button>();
        backButton.onClick.AddListener(backToMainMenu);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
