using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStateExitReloadScene : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
