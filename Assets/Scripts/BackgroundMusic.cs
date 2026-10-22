using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static BackgroundMusic instance;
    static AudioSource audioSource;
    public AudioClip dukeBoast;
    public AudioClip shanking;
    public AudioClip teacher;
    public AudioClip mainTheme;
    public static AudioClip dukeBoastS;
    public static AudioClip shankingS;
    public static AudioClip teacherS;
    public static AudioClip mainThemeS;

    void Awake()
    {
        dukeBoastS = dukeBoast;
        shankingS = shanking;
        teacherS = teacher;
        mainThemeS = mainTheme;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        audioSource = GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapMusic(shanking);
        }
    }
    public static void SwapMusic(AudioClip newSong)
    {
        audioSource.generator = newSong;
        audioSource.Play();
    }
}
