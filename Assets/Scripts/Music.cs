using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; }

    public AudioClip clip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetMusic(gameObject, 0.5f, true);
    }

    private void Start()
    {
        audioSource.Play();
    }

    public void SetMusic(GameObject gameObject, float volume, bool loop)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }
}
