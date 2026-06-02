using UnityEngine;
using UnityEngine.Rendering;

public class ThrowController : MonoBehaviour
{
    public static ThrowController Instance { get; private set; }
    public ThrowModel Model { get; private set; }

    public Transform throwItem;
    private PlayerView view;

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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Model = new ThrowModel();
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

            throwDirection = new Vector2(facingDirection, Model.throwUpForce / Model.throwForce);
            rb.linearVelocity = throwDirection * Model.throwForce;
        }
    }

    public void SwitchLevers()
    {
        if (Model.isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, Model.targetPoint.position, Model.speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, Model.targetPoint.position) < 0.5f)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector2.zero;
                Model.targetPoint = (Model.targetPoint == Model.pointA) ? Model.pointB : Model.pointA;
            }
        }
    }
}
