using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStateExitReloadScene : MonoBehaviour
{
    private GameObject throne, ring, passage;
    private bool Sitting;
    private PlayerView view;

    private void Awake()
    {
        throne = GameObject.Find("Throne");
        ring = GameObject.Find("Ring");
        passage = GameObject.Find("Passage");

        if (StateManager.Instance.leverPulled)
            passage.SetActive(true);
        else 
            passage.SetActive(false);

        view = FindObjectOfType<PlayerView>();
    }

    private void Update()
    {
        Sitting = view.animator.GetBool("Sitting");
    }

    public void ReloadScene()
    {
        if (Sitting)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EatObjects()
    {
        if (!Sitting)
        {
            throne.SetActive(false);
            ring.SetActive(false);
            passage.SetActive(true);
        }
    }
}
