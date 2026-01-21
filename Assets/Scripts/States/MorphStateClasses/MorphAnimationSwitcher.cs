using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class MorphAnimationSwitcher : MonoBehaviour
{
    public static MorphAnimationSwitcher Instance { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController nymphController;
    [SerializeField] private RuntimeAnimatorController squirrelController;
    [SerializeField] private RuntimeAnimatorController butterflyController;
    [SerializeField] private RuntimeAnimatorController beaverController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
        Debug.Log($"MorphAnimationSwitcher destroyed on {gameObject.scene.name}");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var player = GameObject.FindWithTag("Player"); // VERY IMPORTANT: PLAYER IS ONLY GAMEOBJECT WITH PLAYER TAG
        if (player)
        {
            animator = player.GetComponent<Animator>();
            if (!animator) Debug.Log("Animator not found on Player in scene: " + scene.name);
        }
        else
        {
            Debug.Log("Player not found in scene: " + scene.name);
        }
    }

    public void SwitchToSquirrel()
    {
        Debug.Log(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = squirrelController;
        Debug.Log(animator.runtimeAnimatorController);
    }
    public void SwitchToNymph()
    {
        Debug.Log(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = nymphController;
        Debug.Log(animator.runtimeAnimatorController);
    }
    public void SwitchToButterfly()
    {
        Debug.Log(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = butterflyController;
        Debug.Log(animator.runtimeAnimatorController);
    }
    public void SwitchToBeaver()
    {
        Debug.Log(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = beaverController;
        Debug.Log(animator.runtimeAnimatorController);
    }
}