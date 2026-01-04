using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public enum GameState
{
    GameStart, //main menu
    Playing, //contains mini classes for transforming into different animals
    Paused, //menu for both dev work and for pauisng and resuming the game
    GameOver //mini main menu
}

public class GameStateManager : MonoBehaviour
{
    public GameState currentState = GameState.Playing;

    private void Update()
    {
        switch (currentState)
        {
            case GameState.GameStart:

                break;

            case GameState.Playing:

                break;

            case GameState.Paused:

                break;

            case GameState.GameOver:

                break;
        }
    }
}