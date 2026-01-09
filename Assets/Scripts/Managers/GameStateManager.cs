using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameStateMachine gameStateMachine;
    [SerializeField] private PauseMenu pauseMenu;

    private void Awake()
    {
        gameStateMachine = new GameStateMachine();
        pauseMenu.Initialize(gameStateMachine);

        gameStateMachine.ChangeState(new PlayingState(gameStateMachine));
    }

    private void Update()
    {
        gameStateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Escape) && !(gameStateMachine.CurrentState is PausedState)) 
        {
            gameStateMachine.ChangeState(new PausedState(gameStateMachine, pauseMenu));
        }
    }
}