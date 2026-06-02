using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour 
{
    public static StateManager Instance { get; private set; }
    public GameObject exit;
    public bool leverPulled = false;

    private GameObject throne, ring, passage;

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
        exit = GameObject.Find("Exit");
        throne = GameObject.Find("Throne");
        ring = GameObject.Find("Ring");
        passage = GameObject.Find("Passage");

        if (exit != null)
        {
            if (!leverPulled)
            {
                exit.SetActive(false);
            }
            else
            {
                exit.SetActive(true);
                if (throne != null)
                    throne.SetActive(false);

                if (ring != null)
                    ring.SetActive(false);

                if (passage != null)
                {
                    Collider2D collider = passage.GetComponent<Collider2D>();
                    collider.enabled = false;
                }
            }
        }
    }
}
