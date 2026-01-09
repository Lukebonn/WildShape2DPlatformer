using UnityEngine;
using System;

public class MorphAnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController nymphController;
    [SerializeField] private RuntimeAnimatorController squirrelController;
    [SerializeField] private RuntimeAnimatorController butterflyController;
    [SerializeField] private RuntimeAnimatorController beaverController;


    public void SwitchToSquirrel()
    {
        animator.runtimeAnimatorController = squirrelController;
    }
    public void SwitchToNymph()
    {
        animator.runtimeAnimatorController = nymphController;
    }
    public void SwitchToButterfly()
    {
        animator.runtimeAnimatorController = butterflyController;
    }
    public void SwitchToBeaver()
    {
        animator.runtimeAnimatorController = beaverController;
    }
}