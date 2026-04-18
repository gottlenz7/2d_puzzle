using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform throwItem;

    private float throwForce = 20f, throwUpForce = 5f;
    private float facingDirection;
    private Vector2 throwDirection;
    private Rigidbody2D rb;
    private Collider2D itemCollider;

    private void Awake()
    {
        throwItem = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<Collider2D>();
    }

    public void ThrowItem()
    {
        if (transform.parent == Player.Instance.holdPosition)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            itemCollider.enabled = true;

            Player.Instance.heldItem = null;

            if (PlayerVisual.Instance.isRight)
                facingDirection = 1;
            else if (PlayerVisual.Instance.isLeft)
                facingDirection = -1;
            else
                facingDirection = 0;

            throwDirection = new Vector2(facingDirection, throwUpForce / throwForce);
            rb.linearVelocity = throwDirection * throwForce;
        }
    }
}
