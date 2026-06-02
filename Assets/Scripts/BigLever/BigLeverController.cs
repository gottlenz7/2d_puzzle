using UnityEngine;

public class BigLeverController : MonoBehaviour
{
    public Transform item;

    private static BigLeverModel model;

    private bool isUsed = false;

    private void Awake()
    {
        model = new BigLeverModel();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, item.position) < 0.5f)
        {
            if (!isUsed)
            {
                Activate();
                isUsed = true;
            }
        }
        else
            isUsed = false;
    }

    private void Activate()
    {
        model.Toggle();
        BigLeverView.Instance.ActivateLever(model.isUsed, model.firstHit, transform);
    }
}
