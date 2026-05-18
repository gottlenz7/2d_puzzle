using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public Transform throwItem;

    private PlayerView view;

    private float throwForce = 20f, throwUpForce = 5f;
    private float facingDirection;
    private Vector2 throwDirection;
    private Rigidbody2D rb;
    private Collider2D itemCollider;

    public void Init(PlayerView view)
    {
        this.view = view;
    }

    private void Awake()
    {
        throwItem = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<Collider2D>();
    }

    public void ThrowItem()
    {
        if (transform.parent == view.holdPosition)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            itemCollider.enabled = true;

            view.heldItem = null;

            if (view.isRight)
                facingDirection = 1;
            else if (view.isLeft)
                facingDirection = -1;
            else
                facingDirection = 0;

            throwDirection = new Vector2(facingDirection, throwUpForce / throwForce);
            rb.linearVelocity = throwDirection * throwForce;
        }
    }
}
