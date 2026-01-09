using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayingState : IState
{
    private readonly GameStateMachine gameStateMachine;

    public MorphState morphState = MorphState.Nymph;
    public static event Action<MorphState> OnMorphStateChanged;
    private MorphAnimationSwitcher maSwitcher;

    public PlayingState(GameStateMachine gsm)
    {
        gameStateMachine = gsm;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            morphState = MorphState.Nymph;
            UpdateMorphState(morphState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            morphState = MorphState.FlyingSquirrel;
            UpdateMorphState(morphState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            morphState = MorphState.Beaver;
            UpdateMorphState(morphState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            morphState = MorphState.Gorilla;
            UpdateMorphState(morphState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            morphState = MorphState.Butterfly;
            UpdateMorphState(morphState);
        }
    }

    public void Exit()
    {

    }

    public void UpdateMorphState(MorphState newMorphState) 
    {
        morphState = newMorphState;

        switch (morphState)
        {
            case MorphState.Nymph:
                HandleNymphState();
                break;
            case MorphState.FlyingSquirrel:
                HandleFlyingSquirrel();
                break;
            case MorphState.Beaver:
                HandleBeaver();
                break;
            case MorphState.Gorilla:
                HandleGorilla();
                break;
            case MorphState.Butterfly:
                HandleButterfly();
                break;
        }
        OnMorphStateChanged?.Invoke(newMorphState);
    }


    // --------- what other classes do you want called and what is the step by step (compartimentalize by doing all steps here) ---------------------------
    private void HandleButterfly()
    {
        Debug.Log("WildShaped into Butterfly");

    }

    private void HandleGorilla()
    {
        Debug.Log("WildShaped into Gorilla");
    }

    private void HandleBeaver()
    {
        Debug.Log("WildShaped into Beaver");
    }

    private void HandleFlyingSquirrel()
    {
        Debug.Log("WildShaped into Flying Squirrel");
        maSwitcher.SwitchToSquirrel();
        //leave comment for event triggered UI in SquuirrelUI.cs

    }

    private void HandleNymphState()
    {
        Debug.Log("WildShaped into Nymph");
        maSwitcher.SwitchToNymph();
        //leave comment for event triggered UI in NymphUI.cs
    }
}

public enum MorphState
{
    Nymph,
    FlyingSquirrel,
    Beaver,
    Gorilla,
    Butterfly
}