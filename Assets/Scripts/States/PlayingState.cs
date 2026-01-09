using UnityEngine;

public class PlayingState : IState
{
    private readonly GameStateMachine gameStateMachine;

    public PlayingState(GameStateMachine gsm)
    {
        gameStateMachine = gsm;
    }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}