using System.Linq;
using UnityEngine;

public class BigLever : MonoBehaviour
{
    //public Transform lever1, lever2;
    //public Transform item;

    //private float newX;
    //private Vector3 scale1, scale2;

    //private static bool firstHit = true;
    //private bool isUsed = false;
    //private GameObject[] pinkBoxes, greenBoxes, allBoxes;

    //private void Awake()
    //{
    //    pinkBoxes = GameObject.FindGameObjectsWithTag("PinkBox");
    //    greenBoxes = GameObject.FindGameObjectsWithTag("GreenBox");

    //    allBoxes = pinkBoxes.Concat(greenBoxes).ToArray();
    //}

    //private void Update()
    //{
    //    if (Vector2.Distance(transform.position, item.position) < 0.5f)
    //    {
    //        if (!isUsed)
    //        {
    //            ActivateLever();
    //            isUsed = true;
    //        }
    //    }
    //    else
    //        isUsed = false;
    //}

    //private void ActivateLever()
    //{
    //    if ((transform == lever1 && firstHit) || !firstHit)
    //    {
    //        firstHit = false;

    //        Throw.Instance.isHit = true;
    //        Throw.Instance.targetPoint = transform;
    //        Throw.Instance.pointA = transform;
    //        Throw.Instance.pointB = (transform == lever1) ? lever2 : lever1;

    //        newX = transform.localScale.x * -1f;

    //        scale1 = lever1.localScale;
    //        scale1.x = newX;
    //        lever1.localScale = scale1;

    //        scale2 = lever2.localScale;
    //        scale2.x = newX;
    //        lever2.localScale = scale2;

    //        BoxVisibility();
    //    }
    //}

    //private void BoxVisibility()
    //{
    //    foreach (GameObject box in allBoxes)
    //    {
    //        if (newX == 1f)
    //        {
    //            if (box.tag == "PinkBox")
    //                box.SetActive(false);
    //            else
    //                box.SetActive(true);
    //        }
    //        else
    //        {
    //            if (box.tag == "PinkBox")
    //                box.SetActive(true);
    //            else
    //                box.SetActive(false);
    //        }
    //    }
    //}
}
