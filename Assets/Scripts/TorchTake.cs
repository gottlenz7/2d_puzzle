using Unity.VisualScripting;
using UnityEngine;

public class TorchTake : MonoBehaviour
{
    public GameObject lever;

    private void Awake()
    {
        lever.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(lever.transform.position, transform.position);
        if (distance < 5f)
            lever.SetActive(true);
        else
            lever.SetActive(false);
    }
}
