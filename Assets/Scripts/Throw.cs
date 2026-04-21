using UnityEngine;

public class Throw : MonoBehaviour
{
    public static Throw Instance { get; private set; }

    public Transform throwItem;
    public Transform pointA, pointB;
    public Transform targetPoint;
    public bool isHit = false;

    private float throwForce = 10f, throwUpForce = 3f;
    private float facingDirection;

    private float speed = 5f;

    private Vector2 throwDirection;
    private Rigidbody2D rb;
    private Collider2D itemCollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        throwItem = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        itemCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        SwitchLevers();
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

    public void SwitchLevers()
    {
        if (isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector2.zero;
                targetPoint = (targetPoint == pointA) ? pointB : pointA;
            }
        }
    }
}
