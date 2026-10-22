using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static BackgroundMusic instance;
    AudioSource audioSource;
    public AudioClip dukeBoast;
    public AudioClip shanking;
    public AudioClip teacher;
    public AudioClip mainTheme;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwapMusic()
    {
        
    }
}
