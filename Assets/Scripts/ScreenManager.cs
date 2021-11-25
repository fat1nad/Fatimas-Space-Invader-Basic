// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    public Animator backgroundAnim;
    public GameObject mainMenu;
    public GameObject menuSettingsScreen;
    public GameObject settings;
    public GameObject gameScreen;
    public GameObject quitPrompt;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        GoToMainMenu();
    }
    public void GoToMainMenu()
    {
        mainMenu.SetActive(true);
        backgroundAnim.speed = 4.0f;
        menuSettingsScreen.SetActive(false);
        settings.SetActive(false);
        gameScreen.SetActive(false);
        quitPrompt.SetActive(false);
    }

    public void GoToGameScreen()
    {
        gameScreen.SetActive(true);
        backgroundAnim.speed = 1.0f;
        mainMenu.SetActive(false);
    }

    public void GoToMenuSettings()
    {
        menuSettingsScreen.SetActive(true);
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GoToQuitPrompt()
    {
        quitPrompt.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void EndApplication()
    {
        Application.Quit();
    }
}
