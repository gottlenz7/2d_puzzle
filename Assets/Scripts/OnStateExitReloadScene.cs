using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStateExitReloadScene : MonoBehaviour
{
    private GameObject throne, ring, passage;
    private bool Sitting;

    private void Awake()
    {
        throne = GameObject.Find("Throne");
        ring = GameObject.Find("Ring");
        passage = GameObject.Find("Passage");
        passage.SetActive(false);
    }

    private void Update()
    {
        Sitting = PlayerVisual.Instance.animator.GetBool("Sitting");
    }

    public void ReloadScene()
    {
        if (Sitting) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EatObjects()
    {
        throne.SetActive(false);
        ring.SetActive(false);
        passage.SetActive(true);
    }
}
