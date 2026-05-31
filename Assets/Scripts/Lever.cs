using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    public Transform lever;
    public GameObject exit;

    private PlayerView view;

    private float rotateLevel = 90f;
    private bool isUsed = false;

    public void Init(PlayerView view)
    {
        this.view = view;
    }

    public void SwithchLever()
    {
        float distance = Vector3.Distance(transform.position, view.position);

        if (distance < 3f)
        {
            if (!isUsed)
            {
                lever.Rotate(0f, 0f, rotateLevel);
                isUsed = true;

                if (SceneManager.GetActiveScene().name == "Level1.1")
                    StateManager.Instance.leverPulled = true;

                if (exit != null)
                    exit.SetActive(true);
            }
            else
            {
                lever.Rotate(0f, 0f, -rotateLevel);
                isUsed = false;

                if (SceneManager.GetActiveScene().name == "Level1.1")
                    StateManager.Instance.leverPulled = false;

                if (exit != null)
                    exit.SetActive(false);
            }
        }
    }
}
