using System;
using UnityEngine;

public class SquirrelUI : MonoBehaviour
{
    [SerializeField] private GameObject UIObj;

    private void Awake()
    {
        PlayingState.OnMorphStateChanged += PlayingStateOnOnMorphStateChanged;
    }

    void OnDestroy()
    {
        PlayingState.OnMorphStateChanged -= PlayingStateOnOnMorphStateChanged;
    }

    private void PlayingStateOnOnMorphStateChanged(MorphState state)
    {
        UIObj.SetActive(state == MorphState.FlyingSquirrel);
    }
}
