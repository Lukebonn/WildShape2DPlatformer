using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip playerMusic;
    public AudioClip rainbowConnection;
    public AudioClip apologySong;


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
    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    var player = GameObject.FindWithTag("Player"); // VERY IMPORTANT: PLAYER IS ONLY GAMEOBJECT WITH PLAYER TAG
    //}
    //private void Start()
    //{
    //    musicSource.clip = rainbowConnection;
    //    musicSource.Play();
    //}

    public void PlayerBanjo()
    {
        musicSource.clip = apologySong;
        musicSource.Play();
    }
    public void PlayerBanjoStop()
    {
        musicSource.Stop();
    }

}
