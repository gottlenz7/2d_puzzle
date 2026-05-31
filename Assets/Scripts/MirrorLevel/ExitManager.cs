using UnityEngine;

public class ExitManager : MonoBehaviour 
{
    public GameObject exit;
    public Receiver[] receivers;

    private void Awake()
    {
        exit.SetActive(false);
    }

    private void Update()
    {
        CheckActive();
    }

    private void CheckActive()
    {
        foreach (var receiver in receivers)
            if (!receiver.activated)
                return;

        exit.SetActive(true);
    }
}
