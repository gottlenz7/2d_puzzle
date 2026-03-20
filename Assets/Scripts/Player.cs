using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    private float speed = 5f, minSpeed = 0.1f, jumpForce = 5f, maxJump = 0f;

    public Transform holdPosition;
    public GameObject heldItem = null;
    private Collider2D itemCollider;


    private Vector2 lastDirection = Vector2.down;
    public bool IsRight => lastDirection.x > 0 && Input.GetKey(KeyCode.D);
    public bool IsLeft => lastDirection.x < 0 && Input.GetKey(KeyCode.A);
    public bool Jump => (Input.GetKeyDown(KeyCode.Space));
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        Taketem();
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector = inputVector.normalized;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (inputVector.magnitude > minSpeed)
            lastDirection = inputVector;

        if (Jump && rb.position.y <= maxJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Taketem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
                TryTake();
            else
                DropItem();
        }
    }

    private void TryTake()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                heldItem = collider.gameObject;
                itemCollider = collider;

                Rigidbody2D rbItem = heldItem.GetComponent<Rigidbody2D>();
                rbItem.isKinematic = true;
                rbItem.linearVelocity = Vector2.zero;

                itemCollider.enabled = false;

                heldItem.transform.SetParent(holdPosition);
                heldItem.transform.localPosition = Vector2.zero;
                heldItem.transform.localRotation = Quaternion.identity;

                break;
            }
        }
    }

    private void DropItem()
    {
        heldItem.transform.SetParent(null);

        Rigidbody2D rbItem = heldItem.GetComponent<Rigidbody2D>();
        rbItem.isKinematic = false;
        
        itemCollider.enabled = true;

        heldItem = null;
    }
}
