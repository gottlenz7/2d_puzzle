using UnityEngine;

public class Lever : MonoBehaviour 
{
    public Transform lever;

    private float rotateLevel = 90f;
    private bool isUsed = false;

    public void SwithchLever()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (distance < 3f)
        {
            if (!isUsed)
            {
                lever.Rotate(0f, 0f, rotateLevel);
                isUsed = true;
            }
            else
            {
                lever.Rotate(0f, 0f, -rotateLevel);
                isUsed = false;
            }
        }

    }
}
