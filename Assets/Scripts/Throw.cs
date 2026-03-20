using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Throw : MonoBehaviour
{
    private float throwForce = 20f, throwUpForce = 5f;
    private float facingDirection;
    private Vector2 throwDirection;
    private Rigidbody2D rb;
    private Collider2D itemCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (transform.parent == Player.Instance.holdPosition)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.SetParent(null);
                rb.isKinematic = false;
                itemCollider.enabled = true;

                Player.Instance.heldItem = null;

                if (Player.Instance.IsRight)
                    facingDirection = 1;
                else if (Player.Instance.IsLeft)
                    facingDirection = -1;
                else
                    facingDirection = 0;

                throwDirection = new Vector2(facingDirection, throwUpForce / throwForce);
                rb.linearVelocity = throwDirection * throwForce;
            }
        }
    }
}
