using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    private GameStateMachine gameStateMachine;

    public void Initialize(GameStateMachine gsm)
    {
        gameStateMachine = gsm;
    }

    public void ResumeBtn()
    {
        Show();
        gameStateMachine.CurrentState.Exit();
        gameStateMachine.ChangeState(new PlayingState(gameStateMachine));
    }

    public void MainMenuBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Show()
    {
        container.SetActive(true);
    }
    public void Hide()
    {
        container.SetActive(false);
    }
}
