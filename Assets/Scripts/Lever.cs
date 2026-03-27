using UnityEngine;

public class Lever : MonoBehaviour 
{
    public Transform lever;

    private float rotateLevel = 90f;
    private bool isUsed = false;

    private void Update()
    {
        SwithchLever();
    }

    private void SwithchLever()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (distance < 2.3f)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isUsed)
            {
                lever.Rotate(0f, 0f, rotateLevel);
                isUsed = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isUsed)
            {
                lever.Rotate(0f, 0f, -rotateLevel);
                isUsed = false;
            }
        }
    }
}
