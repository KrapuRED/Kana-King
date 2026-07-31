using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource BGMVLCC;
    [SerializeField] private AudioSource SFX;

    [SerializeField] private AudioClip bgmMusic;
    [SerializeField] private AudioClip vlccMusic;
    public AudioClip walk;
    public AudioClip sword;

    private void Start()
    {
        PlayBGM(bgmMusic);
    }

    public void PlayBGM(AudioClip clip)
    {
        if(clip != null)
        {
            BGM.clip = clip;
            BGM.Play();
        }
    }
    public void PlayBGMVLCC()
    {
        if (vlccMusic != null)
        {
            BGMVLCC.clip = vlccMusic;
            BGMVLCC.Play();
            BGM.mute = true;
        }
    }
    public void StopBGMVLCC()
    {
        BGMVLCC.clip = vlccMusic;
        BGMVLCC.Play();
        BGM.mute = false;

    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFX.PlayOneShot(clip);
        }
    }
}
