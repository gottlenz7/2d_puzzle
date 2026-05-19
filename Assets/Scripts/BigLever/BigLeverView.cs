using UnityEngine;

public class BigLeverView : MonoBehaviour
{
    public static BigLeverView Instance { get; private set; }

    public Transform lever1, lever2;

    private GameObject[] pinkBoxes;
    private GameObject[] greenBoxes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        pinkBoxes = GameObject.FindGameObjectsWithTag("PinkBox");
        greenBoxes = GameObject.FindGameObjectsWithTag("GreenBox");
    }

    public void ActivateLever(bool isUsed, bool firstHit, Transform activeLever)
    {
        if ((activeLever == lever1 && firstHit) || !firstHit)
        {
            ThrowController.Instance.Model.isHit = true;
            ThrowController.Instance.Model.targetPoint = activeLever;
            ThrowController.Instance.Model.pointA = activeLever;
            ThrowController.Instance.Model.pointB = (activeLever == lever1) ? lever2 : lever1;

            float newX = activeLever.localScale.x * -1f;

            FlipLever(lever1, newX);
            FlipLever(lever2, newX);

            BoxVisibility(isUsed);
        }
    }

    private void FlipLever(Transform lever, float x)
    {
        Vector3 scale = lever.localScale;
        scale.x = x;
        lever.localScale = scale;
    }

    private void BoxVisibility(bool isUsed)
    {
        foreach (var box in pinkBoxes)
            box.SetActive(!isUsed);

        foreach (var box in greenBoxes)
            box.SetActive(isUsed);
    }
}
