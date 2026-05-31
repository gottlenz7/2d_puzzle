using UnityEngine;

public class Receiver : MonoBehaviour
{
    public bool activated = false;

    public void Hit()
    {
        activated = true;
    }
}
