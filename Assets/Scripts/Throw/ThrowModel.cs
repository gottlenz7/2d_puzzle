using UnityEngine;

public class ThrowModel
{
    public Transform throwItem;
    public Transform pointA, pointB;
    public Transform targetPoint;
    public bool isHit = false;

    public float throwForce = 7f, throwUpForce = 3f;
    public float speed = 5f;
}
