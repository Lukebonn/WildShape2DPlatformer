using UnityEngine;

public class GameStateMachine
{
    public IState CurrentState { get; private set; } //requires that all changes to state be run through this class to avoid subtler errors
    private IState previousState;

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        previousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ResumePrevState() //mainly is for a state like pause that requires a return to whatever just happened
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
    }

    public void Update() //if CurrentState exists update it specifically delegating it to whatever CurrentState is
    {
        CurrentState?.Update();
    }

}

