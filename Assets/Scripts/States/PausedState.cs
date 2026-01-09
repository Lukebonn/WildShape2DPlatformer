using UnityEngine;

public class PausedState : IState
{
    private readonly GameStateMachine gameStateMachine;
    private readonly PauseMenu pauseMenu;

    public PausedState(GameStateMachine gsm, PauseMenu menu)
    {
        gameStateMachine = gsm;
        pauseMenu = menu;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        pauseMenu.Show();
        Debug.Log("enter paused state");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameStateMachine.ResumePrevState();
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        pauseMenu.Hide();
    }
}