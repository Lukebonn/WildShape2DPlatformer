using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour //helper file for the Main Menu Scene
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        //will not work in the editor (will only work when completely exported)
        Application.Quit();
    }
}
