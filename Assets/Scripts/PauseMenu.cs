using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeBtn()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
