using UnityEngine;

public interface IState //base interface for the main game state machine
{
    void Enter();
    void Update();
    void Exit();
}